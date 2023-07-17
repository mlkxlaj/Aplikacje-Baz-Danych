namespace Zadanie9.Middleware;

public class ErrorLogg
{
    private readonly RequestDelegate _next;
    private const string LogFilePath = "logs.txt";

    public ErrorLogg(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            await LogExceptionToFile(ex);
            throw;
        }
    }

    private async Task LogExceptionToFile(Exception exception)
    {
        var logMessage = $"{DateTime.UtcNow}: {exception.Message}\n{exception.StackTrace}\n\n";

        await File.AppendAllTextAsync(LogFilePath, logMessage);
    }
}