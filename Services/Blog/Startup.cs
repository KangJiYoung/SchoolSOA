using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using SchoolSOA.Common.Options;
using SchoolSOA.Services.Blog.Entities;

namespace Blog
{
    public class Startup
    {
        private readonly IConfiguration configuration;

        public Startup(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<SqlServerConnectionOptions>(configuration.GetSection("Database"));
            var serviceProvider = services.BuildServiceProvider();
            var sqlServerConnectionOptions = serviceProvider.GetRequiredService<IOptions<SqlServerConnectionOptions>>().Value;

            services.AddDbContextPool<BlogDbContext>(it => it.UseSqlServer(sqlServerConnectionOptions.ConnectionString));

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

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseAuthentication();
            app.UseMvcWithDefaultRoute();
        }
    }
}
