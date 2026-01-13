using System.Net;
using System.Text.Json;
using Booking.Infrastructure.ProblemDetail;
using Microsoft.Extensions.Logging;

namespace Booking.Infrastructure.Middlewares
{
    public class ApiExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ApiExceptionHandlingMiddleware> _logger;

        public ApiExceptionHandlingMiddleware(RequestDelegate next, ILogger<ApiExceptionHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            string result;

            if (ex is DomainException e)
            {
                bool isNotFound = e.Code?.Contains("NOT_FOUND", StringComparison.OrdinalIgnoreCase) == true;
                int statusCode = isNotFound ? (int)HttpStatusCode.NotFound : (int)HttpStatusCode.BadRequest;

                var problemDetails = new CustomValidationProblemDetails(new Dictionary<string, string[]>
                {
                    { e.Target ?? "domain", [e.Code ?? "DOMAIN_ERROR"] }
                })
                {
                    Type = isNotFound ? "https://tools.ietf.org/html/rfc7231#section-6.5.4" : "https://tools.ietf.org/html/rfc7231#section-6.5.1",
                    Title = isNotFound ? "Not Found" : "One or more validation errors occurred.",
                    Detail = isNotFound ? "The requested resource was not found." : "One or more domain validation errors occurred.",
                    Status = statusCode,
                    Instance = context.Request.Path,
                };
                context.Response.StatusCode = statusCode;
                result = JsonSerializer.Serialize(problemDetails);
            }
            else
            {
                _logger.LogError(ex, $"An unhandled exception has occurred, {ex.Message}");
                var problemDetails = new ProblemDetails
                {
                    Type = "https://tools.ietf.org/html/rfc7231#section-6.6.1",
                    Title = "Internal Server Error.",
                    Status = (int)HttpStatusCode.InternalServerError,
                    Instance = context.Request.Path,
                    Detail = "Internal server error occurred!"
                };
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                result = JsonSerializer.Serialize(problemDetails);
            }

            context.Response.ContentType = "application/json";
            await context.Response.WriteAsync(result);
        }
    }
}