using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Scheduler.Data;
using Scheduler.Models;
using Scheduler.Services;
using Scheduler.Hubs;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Hangfire;

namespace Scheduler
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

           //services.AddHangfire(configuration =>
           //    configuration.UseSqlServerStorage("Server=(localdb)\\mssqllocaldb;Trusted_Connection=True;MultipleActiveResultSets=true"));

            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            //services.AddTransient<ClaimsPrincipal>(
            //   s => s.GetService<IHttpContextAccessor>().HttpContext.User);

            //services.AddAuthentication(o =>
            //{
            //    o.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            //});
            //services.AddAuthentication(sharedOptions =>
            //{
            //    sharedOptions.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            //    sharedOptions.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            //    // sharedOptions.DefaultChallengeScheme = OpenIdConnectDefaults.AuthenticationScheme;
            //});
            //services.AddAuthentication(IdentityServerAuthenticationDefaults.AuthenticationScheme);
            //        services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
            //.AddCookie();

            services.AddScoped<IDoctorList, DoctorList>();
            services.AddScoped<IDoctorWorkHoursList, DoctorWorkHourList>();

            services.AddAuthentication().AddFacebook(facebookOptions =>
            {
                facebookOptions.AppId = Configuration.GetSection("Authentication")["FacebookId"];
                facebookOptions.AppSecret = Configuration.GetSection("Authentication")["FacebookSecret"];

            }).AddGoogle(googleOptions =>
            {
                googleOptions.ClientId = Configuration.GetSection("Authentication")["GoogleId"];
                googleOptions.ClientSecret = Configuration.GetSection("Authentication")["GoogleSecret"];
            });
           //.AddCookie();

            // Add application services.
            services.AddTransient<IEmailSender, EmailSender>();

            services.AddMvc();
            services.AddSignalR();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IServiceProvider serviceProvider)
        {
            //GlobalConfiguration.Configuration
            //.UseSqlServerStorage("DefaultConnection");

           //app.UseHangfireDashboard();
           //app.UseHangfireServer();

            if (env.IsDevelopment())
            {
                app.UseBrowserLink();
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();
           
            app.UseAuthentication();

            app.UseSignalR(route =>
            {
                route.MapHub<ChatHub>("/chat");
            });

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
