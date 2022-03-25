using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using BlogApplication.Data;
using BlogApplication.Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.CodeAnalysis.FlowAnalysis;
using Microsoft.EntityFrameworkCore;
using ReflectionIT.Mvc.Paging;
using Newtonsoft.Json;

namespace BlogApplication
{
    public class Startup
    {
        private readonly IConfiguration _configuration;

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddIdentity<BlogUser, IdentityRole>(cfg =>
                {
                    cfg.User.RequireUniqueEmail = true;
                })
                .AddEntityFrameworkStores<BlogContext>();
            services.AddDbContext<BlogContext>(cfg =>
                cfg.UseSqlServer(_configuration.GetConnectionString("BlogDb")));
            services.AddTransient<BlogSeeder>();
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddPaging(options => {
                options.ViewName = "Bootstrap4";
                options.PageParameterName = "pageindex";
            });
            services.AddScoped<IBlogRepository, BlogRepository>();
            services.AddControllersWithViews()
                .AddRazorRuntimeCompilation()
                .AddNewtonsoftJson(cfg => cfg.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
