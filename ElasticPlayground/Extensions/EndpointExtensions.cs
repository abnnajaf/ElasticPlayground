using ElasticPlayground.Endpoints;

namespace ElasticPlayground.Extensions;

public static class EndpointExtensions
{
    internal static void MapEndpoints(this WebApplication app)
    {
        LogMessageExample.MapEndpoints(app);
        LogScopeExample.MapEndpoints(app);
        LogExceptionExample.MapEndpoints(app);
        LogLevelExample.MapEndpoints(app);
    }
}