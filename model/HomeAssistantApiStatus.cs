using System.Text.Json.Serialization;

namespace project_wsa_1.model
{
    public class HomeAssistantApiStatus
    {
        [JsonPropertyName("message")]
        public string Message { get; set; }
    }
}