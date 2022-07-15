using BehaviourManagementSystem_API.Data.EF;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace BehaviourManagementSystem_API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();
            // migrate db when run host - Writer DuyLH4
            var services = (IServiceScopeFactory)host.Services.GetService(typeof(IServiceScopeFactory));
            using(var db = services.CreateScope().ServiceProvider.GetService<ApplicationDbContext>())
            {
                db.Database.Migrate();
            }
            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
