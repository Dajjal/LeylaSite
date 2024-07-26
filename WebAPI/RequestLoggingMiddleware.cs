namespace WebAPI;

public class RequestLoggingMiddleware(RequestDelegate next)
{
    public async Task InvokeAsync(HttpContext context)
    {
        // Логирование входящего запроса
        var request = context.Request;
        var requestBodyContent = await new StreamReader(request.Body).ReadToEndAsync();
        Console.WriteLine($"Incoming Request: {request.Method} {request.Path} {requestBodyContent}");

        // Перезапись потока, чтобы другие middleware могли прочитать тело запроса
        request.Body = new MemoryStream(System.Text.Encoding.UTF8.GetBytes(requestBodyContent));
        await next(context);
    }
}