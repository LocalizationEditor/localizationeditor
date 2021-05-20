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
using Microsoft.AspNetCore.CookiePolicy;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authentication.Cookies;
using LocalizationEditor.Base.Infrastructure;
using LocalizationEditor.Web.Infrastrucutre;
using LocalizationEditor.Web.Middleware;

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

    public void ConfigureServices(IServiceCollection services)
    {
      services.AddMvc();
      services.AddSwaggerGen();
      services.AddSpaStaticFiles(configuration => { configuration.RootPath = "ClientApp/dist"; });
      services.AddTransient<ILoginService, LoginService>();
      services.AddOptions(
        _configuration,
        Assembly.GetExecutingAssembly(),
        new HostingOption(
          _hostEnvironment.IsProduction(),
          _hostEnvironment.IsStaging(),
          _hostEnvironment.IsDevelopment()));

      services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
        .AddCookie();

      services.AddOptions<CookieAuthenticationOptions>(CookieAuthenticationDefaults.AuthenticationScheme)
        .Configure<ICookiesOptionProvider>((options, myService) =>
        {
          options.Cookie.Name = myService.Key;
        });
    }

    public void ConfigureContainer(ContainerBuilder builder)
    {
#pragma warning disable S1481 // Unused local variables should be removed
      var hack = typeof(LocalizationStringDbModel);
#pragma warning restore S1481 // Unused local variables should be removed
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
        app.UseDeveloperExceptionPage()
           .UseSwagger()
           .UseSwaggerUI(option =>
           {
             option.SwaggerEndpoint("/swagger/v1/swagger.json", "Localization Editor Api V1");
           });
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

      app.UseCookiePolicy(new CookiePolicyOptions
      {
        MinimumSameSitePolicy = SameSiteMode.Strict,
        HttpOnly = HttpOnlyPolicy.Always,
        Secure = CookieSecurePolicy.Always
      });

      app.UseMiddleware<LoggingMiddleware>();

      app.UseAuthentication();
      app.UseAuthorization();

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
  }
}