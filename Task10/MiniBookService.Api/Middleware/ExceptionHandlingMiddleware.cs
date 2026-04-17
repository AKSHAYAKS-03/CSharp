using System.Net;
using System.Text.Json;

//Error code + JSON response


namespace MiniBookService.Api.Middleware;

public class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;
    // calls next middleware / next step

    private readonly ILogger<ExceptionHandlingMiddleware> _logger;

    public ExceptionHandlingMiddleware(
        RequestDelegate next,
        ILogger<ExceptionHandlingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
            //requests goes to next step
        }
        catch (Exception exception)
        {
            _logger.LogError(exception, "Something went wrong");

            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            context.Response.ContentType = "application/json";

            var response = new
            {
                message = "An unexpected error happened. Please try again later."
            };

            await context.Response.WriteAsync(JsonSerializer.Serialize(response));
        }
    }
}
