using System;
using Auth.Application;
using Auth.Persistence;
using LocalizationEditor.Base;
using LocalizationEditor.Base.Auth;
using LocalizationEditor.Base.Auth.Infrastructure;
using LocalizationEditor.Base.Utils.Json;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.SpaServices.AngularCli;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;

namespace Localization
{
  public class Startup
  {
    private readonly IConfiguration _configuration;

    public Startup(IConfiguration configuration)
    {
      _configuration = configuration;
    }

    // This method gets called by the runtime. Use this method to add services to the container.
    public void ConfigureServices(IServiceCollection services)
    {
      services.AddCors(options =>
      {
        options.AddDefaultPolicy(builder =>
        {
          builder
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader();
        });
      });

      AddServices(services);
      AddAuthServices(services);
      services.AddCors(options =>
      {
        options.AddPolicy("AllowAllOrigins",
          builder => builder.AllowAnyOrigin());
      });

      services.AddSwaggerGen();
      services.AddMvc().AddNewtonsoftJson(options => AddJsonSetting(options.SerializerSettings));

      services.AddSpaStaticFiles(configuration => { configuration.RootPath = "ClientApp/dist"; });
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
      if (env.IsDevelopment())
      {
        app.UseCors();
        app.UseDeveloperExceptionPage();
      }
      else
      {
        app.UseExceptionHandler("/Error");
        // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
        app.UseHsts();
      }

      app.UseHttpsRedirection();
      app.UseStaticFiles();
      if (!env.IsDevelopment())
      {
        app.UseSpaStaticFiles();
      }

      app.UseRouting();


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
        // To learn more about options for serving an Angular SPA from ASP.NET Core,
        // see https://go.microsoft.com/fwlink/?linkid=864501

        spa.Options.SourcePath = "ClientApp";

        if (env.IsDevelopment())
        {
          spa.UseAngularCliServer(npmScript: "start");
        }
      });
    }


    private void AddSwagger(IApplicationBuilder app)
    {
      app.UseSwagger();
      app.UseSwaggerUI(c =>
      {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Localization Editor V1");
        c.RoutePrefix = String.Empty;
      });
    }

    private void AddJsonSetting(JsonSerializerSettings settings)
    {
      settings.ContractResolver = new JsonSensitiveContractResolver();
      settings.NullValueHandling = NullValueHandling.Ignore;
      settings.ReferenceLoopHandling = ReferenceLoopHandling.Serialize;
    }

    private void AddServices(IServiceCollection services)
    {
      services.AddBase();
      services.AddAuth(_configuration);
      services.AddAuthApplication();
      services.AddAuthPersistence();
    }

    private void AddAuthServices(IServiceCollection services)
    {
      var sp = services.BuildServiceProvider();
      var authOptionsProvider = sp.GetRequiredService<IAuthOptionsProvider>();
      services.AddAuthentication("JwtDefaultSchema")
        .AddJwtBearer(options =>
        {
          options.RequireHttpsMetadata = true;
          options.TokenValidationParameters = new TokenValidationParameters
          {
            ValidateIssuer = true,
            ValidIssuer = authOptionsProvider.Issuer,
            ValidateAudience = true,
            ValidAudience = authOptionsProvider.Audience,
            ValidateLifetime = true,
            IssuerSigningKey = authOptionsProvider.GetSecurityKey(),
            ValidateIssuerSigningKey = true
          };
        });
    }
  }
}