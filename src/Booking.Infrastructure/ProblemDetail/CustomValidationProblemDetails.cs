using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Text.Json.Serialization;

namespace Booking.Infrastructure.ProblemDetail
{
    public class CustomValidationProblemDetails : ValidationProblemDetails
    {
        public CustomValidationProblemDetails()
        {
        }

        public CustomValidationProblemDetails(IEnumerable<ValidationError> errors)
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
        public new IEnumerable<ValidationError> Errors { get; } = new List<ValidationError>();

        private List<ValidationError> ConvertModelStateErrorsToValidationErrors(ModelStateDictionary modelStateDictionary)
        {
            List<ValidationError> validationErrors = new();

            // Try to capture the body parameter name (e.g. "userInsertDto") so we can build
            // pointers like "/userInsertDto/lastName" for deserialization errors on key "$".
            string? bodyPrefix = null;
            var hasRootDeserializationError = false;

            // Primer recorrido: detectar bodyPrefix y si hay error de deserialización en "$".
            foreach (var entry in modelStateDictionary)
            {
                var key = entry.Key;
                var errors = entry.Value.Errors;

                if (errors.Count == 0)
                {
                    continue;
                }

                if (key == "$")
                {
                    hasRootDeserializationError = true;
                    continue;
                }

                if (string.IsNullOrWhiteSpace(bodyPrefix)
                    && !string.IsNullOrWhiteSpace(key)
                    && !key.Contains('.'))
                {
                    bodyPrefix = key; // e.g. "userInsertDto"
                }
            }

            if (hasRootDeserializationError && modelStateDictionary.TryGetValue("$", out var rootEntry))
            {
                foreach (var error in rootEntry.Errors)
                {
                    foreach (var propertyName in ExtractMissingPropertiesFromDeserializationMessage(error.ErrorMessage))
                    {
                        var finalPointer = $"/{propertyName}";

                        validationErrors.Add(new ValidationError
                        {
                            Pointer = finalPointer,
                            Reason = $"The '{propertyName}' field is required."
                        });
                    }
                }
            }


            bool skipBodyPrefixGenericError = false;
            if (!string.IsNullOrWhiteSpace(bodyPrefix))
            {
                var childPrefix = bodyPrefix + ".";
                skipBodyPrefixGenericError = modelStateDictionary.Keys.Any(k => k.StartsWith(childPrefix, StringComparison.Ordinal));
            }

            foreach (var entry in modelStateDictionary)
            {
                var key = entry.Key;
                var errors = entry.Value.Errors;

                if (errors.Count == 0)
                {
                    continue;
                }

                if (key == "$")
                {
                    continue;
                }

                if (!string.IsNullOrWhiteSpace(bodyPrefix) && key == bodyPrefix)
                {
                    continue;
                }


                string? pointer = null;
                if (!string.IsNullOrWhiteSpace(key))
                {
                    var fieldName = key
                        .Split('.', StringSplitOptions.RemoveEmptyEntries)
                        .LastOrDefault();

                    if (!string.IsNullOrWhiteSpace(fieldName))
                    {
                        pointer = $"/{fieldName}";
                    }
                }

                foreach (var error in errors)
                {
                    validationErrors.Add(new ValidationError
                    {
                        Pointer = pointer,
                        Reason = error.ErrorMessage
                    });
                }
            }

            return validationErrors;
        }

        private static IEnumerable<string> ExtractMissingPropertiesFromDeserializationMessage(string? message)
        {
            if (string.IsNullOrWhiteSpace(message))
            {
                yield break;
            }

            const string marker = "including the following:";
            var index = message.IndexOf(marker, StringComparison.OrdinalIgnoreCase);
            if (index < 0)
            {
                yield break;
            }

            var listPart = message[(index + marker.Length)..].Trim();
            if (string.IsNullOrWhiteSpace(listPart))
            {
                yield break;
            }

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
