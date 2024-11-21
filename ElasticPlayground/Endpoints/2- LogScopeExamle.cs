using System.Diagnostics;

namespace ElasticPlayground.Endpoints;

public class LogScopeExample
{
    internal static void MapEndpoints(WebApplication app)
    {
        var groupBuilder = app.MapGroup("logs")
            .WithTags("EX 2");

        groupBuilder.MapGet("/logs-scope", (ILogger<LogScopeExample> logger) =>
            {
                var stopwatch = Stopwatch.StartNew();

                var book = new
                {
                    Name = ".net 8",
                    Id = Guid.NewGuid()
                };

                using (logger.BeginScope("{BookId}", book.Id))
                {
                    logger.LogInformation("[LogScope] Starting methods.");

                    for (var i = 1; i <= 3; i++)
                    {
                        Thread.Sleep(100);

                        logger.LogInformation("[LogScope] Process {ProcessNumber}", i);
                    }
                }

                stopwatch.Stop();

                using (logger.BeginScope("{Duration}", stopwatch.ElapsedMilliseconds))
                    logger.LogInformation("[LogScope] Finished methods.");

                // logger.LogInformation("[LogScope] Finished methods. {Duration}", stopwatch.ElapsedMilliseconds);

                return Results.Ok();
            })
            .WithOpenApi();
    }
}