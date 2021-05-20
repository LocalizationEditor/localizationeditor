using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NLog.Web;
using System;

namespace LocalizationEditor.Web
{
  public static class Program
  {
    public static void Main(string[] args)
    {
      var logger = NLogBuilder.ConfigureNLog("NLog.config").GetCurrentClassLogger();

      try
      {
        logger.Debug("Start app");
        CreateHostBuilder(args).Build().Run();
      }
      catch (Exception ex)
      {
        logger.Error(ex);
      }
    }

    private static IHostBuilder CreateHostBuilder(string[] args) =>
      Host.CreateDefaultBuilder(args)
        .UseServiceProviderFactory(new AutofacServiceProviderFactory())
        .ConfigureWebHostDefaults(webBuilder => { webBuilder.UseStartup<Startup>(); })
         .ConfigureAppConfiguration((builderContext, config) =>
         {
           config
             .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
             .AddJsonFile($"appsettings.Development.json", optional: true, reloadOnChange: true);
         })
       .ConfigureLogging(loggingBuilder =>
       {
         loggingBuilder.ClearProviders();
         loggingBuilder.SetMinimumLevel(LogLevel.Debug);
       })
      .UseNLog();
  }
}