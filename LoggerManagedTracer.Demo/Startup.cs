using Google.Cloud.Diagnostics.Common;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;

namespace GCP.LoggerManagedTracer.Demo;

public class Startup
{
    private readonly IWebHostEnvironment environment;

    public Startup(IConfiguration configuration, IWebHostEnvironment environment)
    {
        Configuration = configuration;
        this.environment = environment;
    }

    public IConfiguration Configuration { get; }

    // This method gets called by the runtime. Use this method to add services to the container.
    public void ConfigureServices(IServiceCollection services)
    {
        if (environment.IsDevelopment())
        {
            // Add the LoggerManagedTracer.
            services.AddTransient<IManagedTracer, LoggerManagedTracer>();
        }
        if (environment.IsStaging() || environment.IsProduction())
        {
            // Add the default Google Trace implementation here.
            // services.AddGoogleTrace();
        }

        // Setup the log output for the demo.
        services.AddLogging(opt =>
        {
            opt.AddSimpleConsole(options =>
            {
                options.TimestampFormat = "[HH:mm:ss.fff] ";
                options.SingleLine = true;
            });
        });

        services.AddControllers();
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc(
                "v1",
                new OpenApiInfo { Title = "LoggerManagedTracer.Demo", Version = "v1" }
            );
        });
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
            app.UseSwagger();
            app.UseSwaggerUI(c =>
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "LoggerManagedTracer.Demo v1")
            );
        }

        app.UseRouting();

        app.UseAuthorization();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });
    }
}
