using System;
using System.Reflection;
using Autofac;
using AutoMapper.Contrib.Autofac.DependencyInjection;
using LocalizationEditor.Base.Extensions;
using LocalizationEditor.DAL.Models.LocalizationString;
using MediatR.Extensions.Autofac.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.SpaServices.AngularCli;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace LocalizationEditor.Web
{
  public class Startup
  {
    private readonly IConfiguration _configuration;
    private readonly IWebHostEnvironment _hostEnvironment;
    public Startup(IConfiguration configuration, IWebHostEnvironment hostEnvironment)
    {
      _configuration = configuration;
      _hostEnvironment = hostEnvironment;
    }

    private ILifetimeScope AutofacContainer { get; set; }

    public void ConfigureServices(IServiceCollection services)
    {
      services.AddMvc();
      services.AddSwaggerGen();
      services.AddSpaStaticFiles(configuration => { configuration.RootPath = "ClientApp/dist"; });
      services.AddOptions(
        _configuration,
        Assembly.GetExecutingAssembly(),
        new HostingOption(
          _hostEnvironment.IsProduction(),
          _hostEnvironment.IsStaging(),
          _hostEnvironment.IsDevelopment()));
    }

    public void ConfigureContainer(ContainerBuilder builder)
    {
      var x = typeof(LocalizationStringDbModel);
      var executingAssembly = Assembly.GetExecutingAssembly();
      var assemblies = executingAssembly.GetAssemblies().ToArray();
      builder.RegisterAssemblyModules(assemblies);
      builder.RegisterMediatR(assemblies);
      builder.RegisterAutoMapper(assemblies);
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
      if (env.IsDevelopment())
      {
        app.UseDeveloperExceptionPage();
        UseSwagger(app);
      }
      else
      {
        app.UseHsts();
      }

      app.UseHttpsRedirection();
      app.UseStaticFiles();

      if (!env.IsDevelopment())
      {
        app.UseSpaStaticFiles();
      }

      app.UseRouting();

      app.UseEndpoints(endpoints =>
      {
        endpoints.MapControllerRoute(
          name: "default",
          pattern: "{controller}/{action=Index}/{id?}");
      });

      app.UseSpa(spa =>
      {
        spa.Options.SourcePath = "ClientApp";
        spa.Options.StartupTimeout = new TimeSpan(0, 5, 0);

        if (env.IsDevelopment())
        {
          spa.UseAngularCliServer(npmScript: "start");
        }
      });
    }

    private void UseSwagger(IApplicationBuilder app)
    {
      app.UseSwagger();
      app.UseSwaggerUI(option =>
      {
        option.SwaggerEndpoint("/swagger/v1/swagger.json", "Localization Editor Api V1");
      });
    }
  }
}