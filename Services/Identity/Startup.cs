using System;
using System.Text;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using MassTransit;
using MassTransit.Util;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using RabbitMQ.Client;
using SchoolSOA.Common.Options;
using SchoolSOA.Services.Identity.Entities;

namespace SchoolSOA.Services.Identity
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
            var sqlServerConnectionOptions = serviceProvider.GetRequiredService<IOptions<SqlServerConnectionOptions>>().Value;

            services.AddDbContextPool<AuthDbContext>(it => it.UseSqlServer(sqlServerConnectionOptions.ConnectionString));
            services.AddIdentity<AuthUser, IdentityRole>()
                .AddEntityFrameworkStores<AuthDbContext>()
                .AddDefaultTokenProviders();
            
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
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes("7ED0A2330F503C9887017387D1DBB52A9175DECEC88A8AB255E96E680A01C452")),
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

            services.AddMvc();
            
            var builder = new ContainerBuilder();
            builder.Register(c =>
                {
                    return Bus.Factory.CreateUsingRabbitMq(sbc =>
                    {
                        sbc.Host("rabbitmq", "/", h =>
                        {
                            h.Username("guest");
                            h.Password("guest");
                        });

                        sbc.ExchangeType = ExchangeType.Fanout;
                    });
                })
                .As<IBusControl>()
                .As<IBus>()
                .As<IPublishEndpoint>()
                .SingleInstance();
            
            builder.Populate(services);
            ApplicationContainer = builder.Build();

            return new AutofacServiceProvider(ApplicationContainer);
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IApplicationLifetime lifetime)
        {
            app.UseAuthentication();
            app.UseMvcWithDefaultRoute();
            
            var bus = ApplicationContainer.Resolve<IBusControl>();
            var busHandle = TaskUtil.Await(() => bus.StartAsync());
            lifetime.ApplicationStopping.Register(() => busHandle.Stop());
        }
    }
}
