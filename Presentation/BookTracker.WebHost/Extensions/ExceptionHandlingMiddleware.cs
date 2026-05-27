using BookTracker.Domain.Exceptions;
using BookTracker.ValueObjects.Exceptions;

namespace BookTracker.WebHost.Extensions;

public class ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
{
    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await next(context);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Unhandled exception");
            await HandleExceptionAsync(context, ex);
        }
    }

    private static Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        var (status, message) = exception switch
        {
            ArgumentNullValueException e        => (StatusCodes.Status400BadRequest, e.Message),
            BookNotFoundException e             => (StatusCodes.Status404NotFound, e.Message),
            BookAlreadyInListException e        => (StatusCodes.Status409Conflict, e.Message),
            InvalidRatingException e            => (StatusCodes.Status400BadRequest, e.Message),
            InvalidChapterException e           => (StatusCodes.Status400BadRequest, e.Message),
            UserNotModeratorException e         => (StatusCodes.Status403Forbidden, e.Message),
            ArgumentNullOrWhiteSpaceException e => (StatusCodes.Status400BadRequest, e.Message),
            ArgumentLongValueException e        => (StatusCodes.Status400BadRequest, e.Message),
            ArgumentShortValueException e       => (StatusCodes.Status400BadRequest, e.Message),
            _                                   => (StatusCodes.Status500InternalServerError, "An unexpected error occurred.")
        };

        context.Response.ContentType = "application/json";
        context.Response.StatusCode = status;
        return context.Response.WriteAsJsonAsync(new { error = message });
    }
}
