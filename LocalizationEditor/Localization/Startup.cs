using System.Linq;
using System.Reflection;
using Autofac;
using AutoMapper.Contrib.Autofac.DependencyInjection;
using LocalizationEditor.Base.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.SpaServices.AngularCli;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Localization
{
  public class Startup
  {
    public IConfiguration Configuration { get; }
    public ILifetimeScope AutofacContainer { get; private set; }

    public Startup(IConfiguration configuration)
    {
      Configuration = configuration;
    }

    // This method gets called by the runtime. Use this method to add services to the container.
    public void ConfigureServices(IServiceCollection services)
    {
      services.AddMvc();
      services.AddSwaggerGen();
      services.AddSpaStaticFiles(configuration => { configuration.RootPath = "ClientApp/dist"; });
      services.AddOptions(Configuration, Assembly.GetExecutingAssembly());
    }

    public void ConfigureContainer(ContainerBuilder builder)
    {
      var assemblies = Assembly.GetExecutingAssembly().GetAssemblies();
      builder.AddDependencyInjection(Assembly.GetExecutingAssembly());
      builder.RegisterAutoMapper(assemblies.ToArray());
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
      UseSwagger(app);

      app.UseRouting();

      app.UseEndpoints(endpoints =>
      {
        endpoints.MapControllerRoute(
          name: "default",
          pattern: "{controller}/{action=Index}/{id?}");
      });

      app.UseSpa(spa =>
      {
        // To learn more about options for serving an Angular SPA from ASP.NET Core,
        // see https://go.microsoft.com/fwlink/?linkid=864501

        spa.Options.SourcePath = "ClientApp";

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