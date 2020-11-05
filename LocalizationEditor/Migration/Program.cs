using System;
using FluentMigrator.Runner;
using Microsoft.Extensions.DependencyInjection;

namespace Migration
{
  internal class Program
  {
    private static void Main(string[] args)
    {
      var services = CreateServices();
      UpdateDatabase(services);
    }

    private static IServiceProvider CreateServices()
    {
      return new ServiceCollection()
        .AddFluentMigratorCore()
        .ConfigureRunner(options =>
          options
            .AddSqlServer()
            .WithGlobalConnectionString("Server=(localdb)\\MSSQLLocalDB;Database=LocalizationEditor;Trusted_Connection=True;")
            .ScanIn(typeof(Program).Assembly).For.Migrations())
        .AddLogging(i => i.AddFluentMigratorConsole())
        .BuildServiceProvider(false);
    }
    
    private static void UpdateDatabase(IServiceProvider serviceProvider)
    {
      var runner = serviceProvider.GetRequiredService<IMigrationRunner>();
      runner.MigrateUp();
    }
  }
}