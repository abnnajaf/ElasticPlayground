using System.Diagnostics;
using System.Globalization;
using ElasticPlayground.Services;

namespace ElasticPlayground.Endpoints;

public class LogExceptionExample
{
    internal static void MapEndpoints(WebApplication app)
    {
        var groupBuilder = app.MapGroup("logs")
            .WithTags("Ex 3");

        groupBuilder.MapGet("/exceptions", (ILogger<LogExceptionExample> logger, ExampleService exampleService) =>
            {
                try
                {
                    exampleService.ExceptionInLogic();
                }
                catch (Exception ex)
                {
                    logger.LogError("[LogsExceptions] An unexpected error occurred while processing the request." +
                                    ex.Message);
                }

                return Results.Ok();
            })
            .WithOpenApi();


        groupBuilder.MapGet("/exception-details",
                (ILogger<LogExceptionExample> logger, ExampleService exampleService) =>
                {
                    try
                    {
                        exampleService.ExceptionInLogic();
                    }
                    catch (Exception ex)
                    {
                        logger.LogError(ex,
                            "[LogsExceptionDetails] An unexpected error occurred while processing the request.");
                    }

                    return Results.Ok();
                })
            .WithOpenApi();
    }
}