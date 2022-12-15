using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Serilog;
using System;

namespace GLPIService {

    public class Program {

        public static void Main(string[] args) {

            try {
                // load information from the appsettings.json
                // DI not working at this stage
                var configuration = new ConfigurationBuilder()
                    .AddJsonFile("appsettings.json")
                    .Build();

                Log.Logger = new LoggerConfiguration()
                    .ReadFrom.Configuration(configuration)
                    .CreateLogger();


                CreateHostBuilder(args).Build().Run();
                Log.Information("Application started");
            } catch (Exception ex) {
                Log.Fatal(ex, "Application failed to start.");

                throw;
            } finally {
                Log.Information("Application exited.");
                Log.CloseAndFlush();
            }

        }

        public static IHostBuilder CreateHostBuilder(string[] args) {

            return Host.CreateDefaultBuilder(args)
                    .UseSerilog()
                    .ConfigureWebHostDefaults(webBuilder => {
                        webBuilder.UseStartup<Startup>();
                    });
        }
    }
}
