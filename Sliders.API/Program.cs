using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Sliders.API.Data;
using System;
using System.Linq;

namespace Sliders.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();

            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;

                try
                {
                    var context = services.GetRequiredService<SlidersWebContext>();
                    context.Database.Migrate();

                    bool isEmpty = !context.SlidersData.Any();
                    if (isEmpty)
                    {
                        context.SlidersData.Add(new Models.SlidersData
                        {
                            Id = "test",
                            Time = DateTime.UtcNow,
                            Slider1 = -100,
                            Slider2 = -50,
                            Slider3 = 0,
                            Slider4 = 50,
                            Slider5 = 100
                        });
                        context.SaveChanges();
                    }
                }
                catch (Exception ex)
                {
                    var logger = services.GetRequiredService<ILogger<Program>>();
                    logger.LogError(ex, "An error occurred seeding the AppDb.");
                }
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