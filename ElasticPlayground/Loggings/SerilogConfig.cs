using Serilog;
using Serilog.Exceptions;
using Serilog.Sinks.Elasticsearch;

namespace ElasticPlayground.Logging;

public static class SerilogConfig
{
    public static void ConfigureSerilog(this ConfigureHostBuilder hostBuilder, IConfiguration configuration)
    {
        hostBuilder.UseSerilog((context, services, loggerConfiguration) =>
        {
            string? elasticsearchUri = configuration["Elasticsearch:Uri"];
            string elasticsearchIndexFormat = configuration["Elasticsearch:IndexFormat"] ?? "Train.Pax.Next";

            loggerConfiguration
                .ReadFrom.Configuration(configuration)
                .ReadFrom.Services(services) // Automatically add enrichers from DI
                .Enrich.FromLogContext()
                .Enrich.WithExceptionDetails()
                .Enrich.WithMachineName()
                .Enrich.WithProcessId()
                .Enrich.WithProcessName()
                .Enrich.WithThreadId()
                .Enrich.WithEnvironmentName();

            // Write to console
            if (context.HostingEnvironment.IsProduction() is false)
            {
                loggerConfiguration.WriteTo.Console();
            }

            // Write to elasticsearch
            if (elasticsearchUri is not null)
            {
                loggerConfiguration.WriteTo.Elasticsearch(new ElasticsearchSinkOptions(new Uri(elasticsearchUri))
                {
                    AutoRegisterTemplate = true, IndexFormat = $"{elasticsearchIndexFormat}"
                });
            }

            // Show Serilog errors in the console
            Serilog.Debugging.SelfLog.Enable(Console.Error);
        });
    }
}