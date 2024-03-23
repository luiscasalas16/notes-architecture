using System.Text.Json.Serialization;

namespace NCA.Common.Application.Results
{
    public class ResultServerError
    {
        [JsonPropertyName("title")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? Title { get; }

        [JsonPropertyName("status")]
        public int Status { get; }

        [JsonPropertyName("detail")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? Detail { get; }

        public ResultServerError(Exception exception)
        {
            Title = "Internal Server Error";
            Status = Result.ServerErrorCode;
            Detail = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Development" ? exception.ToString() : null;
        }
    }
}
