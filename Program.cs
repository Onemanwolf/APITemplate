using System;
using Microsoft.ApplicationInsights.Extensibility;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Events;

namespace ReservationApi
{
    public class Program
    {
        public static void Main(string[] args)
        {

            var _appInsightConfiguration = new TelemetryConfiguration() { InstrumentationKey = "153fa963-4f48-4576-85a8-2076571d33fe" };




            Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Debug()
            .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
            //.WriteTo.MSSqlServer(Configuration.GetConnectionString("LogConnection"), "_logs", columnOptions: columnOptions)
            .Enrich.FromLogContext()
            .WriteTo.Console()
            .WriteTo.ApplicationInsights(_appInsightConfiguration, TelemetryConverter.Events)
            .CreateLogger();

            Log.Information($"Log this Serilog {DateTime.Now} UTC {DateTime.UtcNow}");

            CreateHostBuilder(args).Build().Run();
        }


        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                }).UseSerilog(); 
    }
}
