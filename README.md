# GCP.LoggerManagedTracer
A simple Google.Cloud.Diagnostics.Common.IManagedTracer implementation to output the span elapsed milliseconds to a Microsoft.Extensions.Logging.ILogger

## Why
Google Cloud Platform is a great tool to understand how your application preforms and how requests propagate in your system. When developing locally and with performance in mind, I was missing a way to utilize the already defined spans to monitor how long they were running. Hence this quick and simple implementation came to mind.

## Usage
```csharp
 public void ConfigureServices(IServiceCollection services)
        {
            if (environment.IsDevelopment())
            {
                // Add the LoggerManagedTracer.
                services.AddTransient<IManagedTracer, LoggerManagedTracer>();
            }
            if (environment.IsStaging() || environment.IsProduction())
            {
                // Add the default Google Trace implmementation here.
                // services.AddGoogleTrace();
            }
            // ...
        }
```
There is a demo in the repository as well. 