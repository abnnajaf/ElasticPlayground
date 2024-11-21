using ElasticPlayground.Services;

namespace ElasticPlayground.Endpoints;

public class LogLevelExample
{
    internal static void MapEndpoints(WebApplication app)
    {
        var groupBuilder = app.MapGroup("logs")
            .WithTags("Ex 4");

        groupBuilder.MapGet("/log-levels", (ILogger<LogLevelExample> logger, ExampleService exampleService) =>
            {

                logger.LogInformation("[LogLevel] step 1 : log information");
                logger.LogDebug("[LogLevel] step 2 : log debug");

                exampleService.LogLevelExample();
                
                return Results.Ok();
            })
            .WithOpenApi();
    }
}