using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Nøsted_Serviceordre_Behandling.Data;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;
using System;

namespace Nøsted_Serviceordre_Behandling
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                })
                .ConfigureServices((hostContext, services) =>
                {
                    // Access the configuration
                    var configuration = hostContext.Configuration;

                    // Replace with your server version and type.
                    var serverVersion = new MySqlServerVersion(new Version(11, 3, 0)); // Update this line with your MariaDB version

                    services.AddDbContext<ApplicationDbContext>(
                        dbContextOptions => dbContextOptions
                            .UseMySql(configuration.GetConnectionString("DefaultConnection"), serverVersion)
                            .LogTo(Console.WriteLine, LogLevel.Information)
                            .EnableSensitiveDataLogging()
                            .EnableDetailedErrors()
                    );
                });
    }
}
