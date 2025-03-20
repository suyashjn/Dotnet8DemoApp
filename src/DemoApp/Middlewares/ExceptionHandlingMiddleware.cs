using DemoApp.Business.CustomExceptions;
using Microsoft.AspNetCore.Mvc;

namespace DemoApp.Middlewares
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandlingMiddleware> _logger;

        public ExceptionHandlingMiddleware(RequestDelegate next,
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
            }
            catch (BadRequestException exception)
            {
                _logger.LogWarning("Bad request: {Message}", exception.Message);
                
                var problemDetails = new ProblemDetails
                {
                    Title = "Bad Request",
                    Status = StatusCodes.Status400BadRequest,
                    Detail = exception.Message
                };

                context.Response.StatusCode = StatusCodes.Status400BadRequest;

                await context.Response.WriteAsJsonAsync(problemDetails);
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, "Exception occurred: {Message}", exception.Message);

                var problemDetails = new ProblemDetails
                {
                    Title = "Server Error",
                    Status = StatusCodes.Status500InternalServerError,
                    Detail = exception.Message
                };

                context.Response.StatusCode = StatusCodes.Status500InternalServerError;

                await context.Response.WriteAsJsonAsync(problemDetails);
            }
        }
    }
}
