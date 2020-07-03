using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using AskApp.Ask.DAL;
using AskApp.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;

namespace AskApp.Web
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
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            // Configuring Services contexts
            services.AddDbContext<AskContext>(options =>
                options.UseSqlite(@"Data Source = C:\Users\Orlane\source\repos\Orl4ne\AskAppMVC6\AskAppMVC6\Services\Ask\AskApp.Ask.DAL\askApp.db"));

            services.AddDbContext<IdentityContext>(options =>
               options.UseSqlite(@"Data Source = C:\Users\Orlane\source\repos\Orl4ne\AskAppMVC6\AskAppMVC6\Services\Identity\AskApp.Identity\askAppIdentity.db"));
            services.AddControllersWithViews();
            services.AddRazorPages();

            //Configuring Service Identity
            services.AddIdentity<AskAppIdentityUser, AskAppUserRole>(options =>
            {
                options.Password.RequiredLength = 8;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = true;
                options.User.RequireUniqueEmail = true;
                options.SignIn.RequireConfirmedEmail = false;
            })
                    .AddRoleManager<RoleManager<AskAppUserRole>>()
                    .AddUserManager<UserManager<AskAppIdentityUser>>()
                    .AddSignInManager()
                    .AddDefaultUI()
                    .AddEntityFrameworkStores<IdentityContext>()
                    .AddDefaultTokenProviders();
        }


        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });
        }
    }
}
