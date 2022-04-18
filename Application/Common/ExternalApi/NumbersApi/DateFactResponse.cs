using System.Text.Json.Serialization;

namespace Application.Common.ExternalApi.NumbersApi
{
    public class DateFactResponse
    {
        [JsonPropertyName("text")]
        public string Text { get; set; }

        [JsonPropertyName("year")]
        public int Year { get; set; }

        [JsonPropertyName("number")]
        public int Number { get; set; }

        [JsonPropertyName("found")]
        public bool Found { get; set; }

        [JsonPropertyName("type")]
        public string Type { get; set; }
    }
}