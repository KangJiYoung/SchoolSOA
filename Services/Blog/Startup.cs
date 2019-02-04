using System;
using System.Text;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Blog.Consumers;
using MassTransit;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using SchoolSOA.Common.Options;
using SchoolSOA.Services.Blog.Entities;
using MassTransit.Util;

namespace Blog
{
    public class Startup
    {
        private readonly IConfiguration configuration;
        
        public IContainer ApplicationContainer { get; private set; }

        public Startup(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.Configure<SqlServerConnectionOptions>(configuration.GetSection("Database"));
            var serviceProvider = services.BuildServiceProvider();
            var sqlServerConnectionOptions =
                serviceProvider.GetRequiredService<IOptions<SqlServerConnectionOptions>>().Value;

            services.AddDbContextPool<BlogDbContext>(it =>
                it.UseSqlServer(sqlServerConnectionOptions.ConnectionString));

            services.AddMediatR();

            services.AddAuthentication(x =>
                {
                    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(x =>
                {
                    x.RequireHttpsMetadata = false;
                    x.SaveToken = true;
                    x.TokenValidationParameters = new TokenValidationParameters
                    {
                        IssuerSigningKey = new SymmetricSecurityKey(
                            Encoding.ASCII.GetBytes(
                                "7ED0A2330F503C9887017387D1DBB52A9175DECEC88A8AB255E96E680A01C452")),
                        ValidateIssuerSigningKey = true,
                        ValidateIssuer = true,
                        ValidIssuer = "Issuer",
                        ValidateAudience = true,
                        ValidAudience = "Audience",
                        ValidateLifetime = true,
                        ClockSkew = TimeSpan.Zero,
                        RequireExpirationTime = false
                    };
                });

            services.AddMvc()
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            var builder = new ContainerBuilder();
            builder.RegisterType<UpdatePostCreatorNameConsumer>();
            builder.RegisterType<UpdateBlogCreatorNameConsumer>();
            builder.Register(context =>
                {
                    var busControl = Bus.Factory.CreateUsingRabbitMq(cfg =>
                    {
                        var host = cfg.Host(new Uri("rabbitmq://rabbitmq/"), h =>
                        {
                            h.Username("guest");
                            h.Password("guest");
                        });

                        cfg.ReceiveEndpoint(host, "blog" + Guid.NewGuid().ToString(), e =>
                        {
                            e.LoadFrom(context);
                        });
                    });

                    return busControl;
                })
                .SingleInstance()
                .As<IBusControl>()
                .As<IBus>();
            
            builder.Populate(services);
            ApplicationContainer = builder.Build();

            return new AutofacServiceProvider(ApplicationContainer);
        }

        public void Configure(IApplicationBuilder app, IApplicationLifetime lifetime)
        {
            app.UseAuthentication();
            app.UseMvcWithDefaultRoute();
            
            var bus = ApplicationContainer.Resolve<IBusControl>();
            var busHandle = TaskUtil.Await(() => bus.StartAsync());
            lifetime.ApplicationStopping.Register(() => busHandle.Stop());
        }
    }
}