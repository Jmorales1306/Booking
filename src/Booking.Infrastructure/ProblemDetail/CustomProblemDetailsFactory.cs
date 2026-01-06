using System.Diagnostics;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Booking.Infrastructure.ProblemDetail
{
    public class CustomProblemDetailsFactory : ProblemDetailsFactory
    {
        public override ProblemDetails CreateProblemDetails(HttpContext httpContext, int? statusCode = null, string? title = null,
            string? type = null, string? detail = null, string? instance = null)
        {
            var problemDetails = new ProblemDetails
            {
                Status = statusCode,
                Title = title,
                Type = type,
                Detail = detail,
                Instance = instance,
            };

            return problemDetails;
        }

        public override ValidationProblemDetails CreateValidationProblemDetails(HttpContext httpContext,
            ModelStateDictionary modelStateDictionary, int? statusCode = null, string? title = null, string? type = null,
            string? detail = null, string? instance = null)
        {
            statusCode ??= StatusCodes.Status400BadRequest;
            type ??= "https://datatracker.ietf.org/doc/html/rfc7231#section-6.5.1";
            instance ??= httpContext.Request.Path;

            title ??= "One or more validation errors occurred.";
            detail ??= "Request body contains invalid or missing fields.";

            var problemDetails = new CustomValidationProblemDetails(modelStateDictionary)
            {
                Status = statusCode,
                Type = type,
                Instance = instance,
                Detail = detail,
            };

            if (title != null)
            {
                problemDetails.Title = title;
            }

            var traceId = Activity.Current?.Id ?? httpContext?.TraceIdentifier;
            if (traceId != null)
            {
                problemDetails.Extensions["traceId"] = traceId;
            }

            return problemDetails;
        }
    }
}