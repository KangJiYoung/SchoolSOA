using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using SchoolSOA.Common.Options;
using SchoolSOA.Services.Identity.Entities;

namespace SchoolSOA.Services.Identity
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

            services.AddDbContextPool<AuthDbContext>(it => it.UseSqlServer(sqlServerConnectionOptions.ConnectionString));
            services.AddIdentity<IdentityUser, IdentityRole>()
                .AddEntityFrameworkStores<AuthDbContext>()
                .AddDefaultTokenProviders();

            services.AddMvc();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseMvcWithDefaultRoute();
        }
    }
}
