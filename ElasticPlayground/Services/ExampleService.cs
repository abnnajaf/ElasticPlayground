namespace ElasticPlayground.Services;

public class ExampleService(ILogger<ExampleService> logger)
{
    public void ExceptionInLogic()
    {
        try
        {
            throw new Exception("Exception message in logic");
        }
        catch (Exception ex)
        {
            throw new Exception("Inner exception Message", ex);
        }
    }
}