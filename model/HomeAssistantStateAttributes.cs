using System.Text.Json.Serialization;

namespace project_wsa_1.model
{
    public class HomeAssistantStateAttributes
    {
        [JsonPropertyName("friendly_name")]
        public string FriendlyName { get; set; }
        
        [JsonPropertyName("brightness")]
        public int Brightness { get; set; }
        
        [JsonPropertyName("white_value")]
        public int WhiteValue { get; set; }
        
        [JsonPropertyName("supported_features")]
        public int SupportedFeatures { get; set; }
    }
}