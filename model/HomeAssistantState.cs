using System;
using System.Text.Json.Serialization;

namespace project_wsa_1.model
{
    public class HomeAssistantState
    {
        [JsonPropertyName("attributes")]
        public HomeAssistantStateAttributes Attributes { get; set; }
        
        [JsonPropertyName("entity_id")]
        public string EntityId { get; set; }
        
        [JsonPropertyName("last_changed")]
        public DateTime LastChangedUTC { get; set; }

        public DateTime LastChanged => LastChangedUTC.ToLocalTime();
        
        [JsonPropertyName("last_updated")]
        public DateTime LastUpdatedUTC { get; set; }

        public DateTime LastUpdated => LastUpdatedUTC.ToLocalTime();
        
        [JsonPropertyName("state")]
        public string State { get; set; }
    }
}