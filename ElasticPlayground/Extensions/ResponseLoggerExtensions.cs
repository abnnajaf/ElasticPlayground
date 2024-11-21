using System.Diagnostics;

namespace ElasticPlayground.Extensions;

public static class ResponseLoggerExtensions
{
    public static IApplicationBuilder UseTraceIdFilter(this IApplicationBuilder builder)
    {
        builder.UseMiddleware<ResponseLoggerMiddleware>();

        return builder;
    }
}

public class ResponseLoggerMiddleware(
    RequestDelegate next,
    ILogger<ResponseLoggerMiddleware> logger,
    IWebHostEnvironment hostEnvironment)
{
    public async Task InvokeAsync(HttpContext context)
    {
        // Register a callback to set the headers just before the response starts
        context.Response.OnStarting(() =>
        {
            string? traceId = Activity.Current?.TraceId.ToString();

            if (traceId is not null &&
                hostEnvironment.IsProduction() is false)
            {
                context.Response.Headers.Append("current-trace-id", traceId);
            }

            return Task.CompletedTask;
        });

        await next(context);
    }
}