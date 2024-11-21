namespace ElasticPlayground.Endpoints;

public class LogMessageExample
{
    internal static void MapEndpoints(WebApplication app)
    {
        var groupBuilder = app.MapGroup("logs")
            .WithTags("EX 1");

        groupBuilder.MapGet("/messages", (ILogger<LogMessageExample> logger) =>
            {
                var elapsedMilliseconds = 300;

                logger.LogInformation("[Message] Finished at " + elapsedMilliseconds);

                return Results.Ok();
            })
            .WithOpenApi();

        groupBuilder.MapGet("/message-templates", (ILogger<LogMessageExample> logger) =>
            {
                var elapsedMilliseconds = 300;

                logger.LogInformation("[MessageTemplate] Finished at {Duration}", elapsedMilliseconds);

                return Results.Ok();
            })
            .WithOpenApi();


        groupBuilder.MapGet("/object-message-templates", (ILogger<LogMessageExample> logger) =>
            {
                var book = new
                {
                    Name = ".net 8",
                    Id = Guid.NewGuid()
                };

                logger.LogInformation("[ObjectMessageTemplate] book contract {Book}", book);

                return Results.Ok();
            })
            .WithOpenApi();
    }
}