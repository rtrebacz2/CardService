using FluentValidation;

namespace CardService.Interceptors;

public class FluentValidationExceptionMiddleware
{
    private readonly RequestDelegate _next;

    public FluentValidationExceptionMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (ValidationException ex)
        {
            context.Response.StatusCode = 400;
            context.Response.ContentType = "application/json";
            var errors = ex.Errors.Select(e => e.ErrorMessage).ToArray();
            await context.Response.WriteAsJsonAsync(new { Errors = errors });
        }
    }
}