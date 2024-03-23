using System.Net.NetworkInformation;
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
        public List<Error>? Errors { get; }

        public ResultClientError(List<Error>? errors)
        {
            Title = "Bad Request Error";
            Status = Result.ClientErrorCode;
            Errors = errors;
        }
    }
}
