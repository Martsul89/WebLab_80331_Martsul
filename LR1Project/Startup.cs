using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebLab.DAL.Entities;
using WebLab.DAL.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using LR1Project.Models;
using WebLabsV05.Services;
using LR1Project.Extensions;

namespace LR1Project
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
            services.AddMvc(option => option.EnableEndpointRouting = false);
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")));
            services.AddIdentity<ApplicationUser, IdentityRole>(opt =>
            {
                opt.Password.RequireLowercase = false;
                opt.Password.RequireNonAlphanumeric = false;
                opt.Password.RequireUppercase = false;
                opt.Password.RequireDigit = false;
            })
            .AddDefaultUI()
            .AddEntityFrameworkStores<ApplicationDbContext>();

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_3_0);

            services.AddDistributedMemoryCache();
            services.AddSession(opt =>
            {
                opt.Cookie.HttpOnly = true;
                opt.Cookie.IsEssential = true;
            });
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<Cart>(sp => CartService.GetCart(sp));

            services.AddDbContext<ÀpplicationDbContext>(options =>
                    options.UseSqlServer(Configuration.GetConnectionString("ÀpplicationDbContext")));



        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ApplicationDbContext context, UserManager<ApplicationUser> userManager,RoleManager<IdentityRole> roleManager)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }
            app.UseLogging();
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();
            
            app.UseAuthentication();
            app.UseSession();

            app.UseMvc(routes => {routes.MapRoute(name: "default", template: "{controller=Home}/{action=Index}/{id?}");});

            DbInitializer.Seed(context, userManager, roleManager)
            .GetAwaiter()
            .GetResult();
        }
    }
}
