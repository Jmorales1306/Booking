using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Booking.Infrastructure.ProblemDetail
{
    public class CustomValidationProblemDetails : ValidationProblemDetails
    {
        public CustomValidationProblemDetails()
        {
        }

        public CustomValidationProblemDetails(IDictionary<string, string[]> errors)
        {
            Errors = errors;
        }

        public CustomValidationProblemDetails(ModelStateDictionary modelState)
        {
            Errors = ConvertModelStateErrorsToValidationErrors(modelState);
        }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public new string? Detail { get; set; }

        [JsonPropertyName("errors")]
        public new IDictionary<string, string[]> Errors { get; init; } = new Dictionary<string, string[]>();

        private static IDictionary<string, string[]> ConvertModelStateErrorsToValidationErrors(ModelStateDictionary modelState)
        {
            var validationErrors = new Dictionary<string, string[]>();

            if (modelState.TryGetValue("$", out var rootEntry))
            {
                var rootErrors = new List<string>();
                foreach (var error in rootEntry.Errors)
                {
                    var missingProperties = ExtractMissingPropertiesFromDeserializationMessage(error.ErrorMessage);
                    foreach (var propertyName in missingProperties)
                    {
                        validationErrors[propertyName] = [$"The '{propertyName}' field is required."];
                    }
                }
            }

            string? bodyPrefix = DetectBodyPrefix(modelState);

            foreach (var (key, entry) in modelState)
            {
                if (key == "$" || entry.Errors.Count == 0) continue;
                if (!string.IsNullOrEmpty(bodyPrefix) && key == bodyPrefix) continue;

                string? pointer = GetJsonPointer(key, bodyPrefix);
                var finalKey = pointer?.TrimStart('/');

                if (!string.IsNullOrEmpty(finalKey))
                {
                    if (finalKey.EndsWith("Dto", StringComparison.OrdinalIgnoreCase))
                    {
                        continue;
                    }

                    var errorsList = new List<string>();

                    foreach (var error in entry.Errors)
                    {
                        var errorMessage = error.ErrorMessage;

                        if (errorMessage.Contains("The JSON value could not be converted", StringComparison.OrdinalIgnoreCase) ||
                            errorMessage.Contains("Could not convert", StringComparison.OrdinalIgnoreCase) ||
                            errorMessage.Contains("is an invalid", StringComparison.OrdinalIgnoreCase) ||
                            errorMessage.Contains("Path: $.", StringComparison.OrdinalIgnoreCase))
                        {
                            errorMessage = "INVALID_FORMAT";
                        }

                        errorsList.Add(errorMessage);
                    }

                    validationErrors[finalKey] = errorsList.ToArray();
                }
            }

            return validationErrors;
        }

        private static string? DetectBodyPrefix(ModelStateDictionary modelState)
        {
            foreach (var key in modelState.Keys)
            {
                if (key == "$" || key.Contains('.')) continue;

                var possiblePrefix = key + ".";
                if (modelState.Keys.Any(k => k.StartsWith(possiblePrefix, StringComparison.Ordinal)))
                {
                    return key;
                }
            }
            return null;
        }

        private static string? GetJsonPointer(string key, string? prefixToRemove)
        {
            if (string.IsNullOrWhiteSpace(key)) return null;

            if (!string.IsNullOrEmpty(prefixToRemove) && key.StartsWith(prefixToRemove + ".", StringComparison.Ordinal))
            {
                key = key[(prefixToRemove.Length + 1)..];
            }

            var fieldName = key.Split('.', StringSplitOptions.RemoveEmptyEntries).LastOrDefault();
            return string.IsNullOrWhiteSpace(fieldName) ? null : $"/{fieldName}";
        }

        private static IEnumerable<string> ExtractMissingPropertiesFromDeserializationMessage(string? message)
        {
            if (string.IsNullOrWhiteSpace(message)) yield break;

            const string marker = "including the following:";
            var index = message.IndexOf(marker, StringComparison.OrdinalIgnoreCase);

            if (index < 0) yield break;

            var listPart = message[(index + marker.Length)..];
            var parts = listPart.Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);

            foreach (var part in parts)
            {
                var name = part.Trim('\'', '"');
                if (!string.IsNullOrWhiteSpace(name))
                {
                    yield return name;
                }
            }
        }
    }
}
