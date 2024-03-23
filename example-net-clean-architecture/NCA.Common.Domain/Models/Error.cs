using System.Text.Json.Serialization;

namespace NCA.Common.Domain.Models
{
    public class Error
    {
        [JsonPropertyName("property")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? Property { get; set; }

        [JsonPropertyName("code")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? Code { get; set; }

        [JsonPropertyName("message")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? Message { get; set; }

        public Error(string code, string message)
            : this(null!, code, message) { }

        public Error(string property, string code, string message)
        {
            Property = property;
            Message = message;
            Code = code;
        }
    }
}
