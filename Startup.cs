using CachingMVC.Models;
using CachingMVC.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace CachingMVC
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            string connectionString = @"Server=(localdb)\MSSQLLocalDB;Database=cacheappdb;Trusted_Connection=True;";
            // внедрение зависимости Entity Framework
            services.AddDbContext<ApplicationContext>(options => options.UseSqlServer(connectionString));
            // внедрение зависимости UserService
            services.AddTransient<UserService>();
            // добавление кэширования
            services.AddMemoryCache();

            services.AddControllersWithViews();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseDeveloperExceptionPage();

            app.UseStaticFiles();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}