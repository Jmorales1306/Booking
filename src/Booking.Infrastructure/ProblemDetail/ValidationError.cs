using System.Text.Json.Serialization;

namespace Booking.Infrastructure.ProblemDetail
{

    public class ValidationError
    {

        [JsonPropertyName("code")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? Code { get; set; }


        [JsonPropertyName("pointer")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? Pointer { get; set; }

        [JsonPropertyName("reason")]
        public string Reason { get; set; } = string.Empty;
    }
}
