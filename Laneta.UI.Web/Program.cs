using System;
using Laneta.EntityFramework;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Laneta.UI.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateWebHostBuilder(args).Build();

            IServiceProvider services = host.Services;
            try
            {
                //To ensure that all migrations applied
                using (var serviceScope = services.CreateScope())
                {
                    var context = serviceScope.ServiceProvider.GetRequiredService<AppDBContext>();
                    //context.Database.EnsureDeleted();
                    bool created = context.Database.EnsureCreated();
                    if (created)
                    {
                        context.Database.Migrate();
                        //Load data
                        DBInitializer.Initialize(context);
                    }
                }
            }
            catch (Exception ex)
            {
                var logger = services.GetRequiredService<ILogger<Program>>();
                logger.LogError(ex, "An error occurred while seeding the database.");
            }

            host.Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            //var hostBuilder = new HostBuilder()
            //  .ConfigureServices(services =>
            //     services.AddHostedService<BackWorkerService>());

            WebHost.CreateDefaultBuilder(args)
                    .UseStartup<Startup>();
    }
}
