using System.Text.Json.Serialization;
using NCA.Common.Domain.Models;

namespace NCA.Common.Application.Results
{
    public class ResultClientError
    {
        [JsonPropertyName("title")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? Title { get; }

        [JsonPropertyName("status")]
        public int Status { get; }

        [JsonPropertyName("errors")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public List<Error>? Errors { get; set; }

        public ResultClientError(List<Error>? errors)
        {
            Title = Result.ClientErrorTitle;
            Status = Result.ClientErrorCode;
            Errors = errors;
        }

        public ResultClientError()
            : this([]) { }
    }
}
