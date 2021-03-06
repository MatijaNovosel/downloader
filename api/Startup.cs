using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using api.Services;
using api.Extensions;
using api.Hubs;

namespace api
{
  public class Startup
  {
    public Startup(IConfiguration configuration)
    {
      Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    public void ConfigureServices(IServiceCollection services)
    {
      services.AddControllers();
      services.AddHttpClient();
      services.AddMvc();
      services.AddSignalR();
      services.AddScoped<ILastFmClient, LastFmClient>();
      services.AddCors(options =>
      {
        options.AddDefaultPolicy(
          builder =>
          {
            builder
              .WithOrigins(new string[] { "http://localhost:8080", "null" })
              .AllowAnyHeader()
              .AllowAnyMethod()
              .AllowCredentials()
              .SetIsOriginAllowedToAllowWildcardSubdomains();
          });
      });
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
      if (env.IsDevelopment())
      {
        app.UseDeveloperExceptionPage();
      }
      app.UseCors();
      app.UseHttpsRedirection();
      app.ConfigureExceptionHandler();
      app.UseRouting();
      app.UseAuthorization();
      app.UseEndpoints(endpoints =>
      {
        endpoints.MapControllers();
        endpoints.MapHub<DownloaderHub>("/downloader-hub");
      });
    }
  }
}
