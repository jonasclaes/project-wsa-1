using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Threading.Tasks;
using project_wsa_1.model;

namespace project_wsa_1
{
    public class WebRequest
    {
        private static readonly HttpClient HttpClient = new HttpClient();
        
        private static void SetupHttpClient()
        {
            // Remove all the default request headers and add our own.
            HttpClient.DefaultRequestHeaders.Clear();
            HttpClient.DefaultRequestHeaders.Accept.Clear();
            HttpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            
            // Set the User-Agent for the requests. In this case I chose the project name as UA.
            HttpClient.DefaultRequestHeaders.Add("User-Agent", "project-wsa-1");

            HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Program.HassApiKey);
        }

        public static async Task<HomeAssistantApiStatus> GetApiState()
        {
            SetupHttpClient();

            // Get the API status.
            var streamTask = HttpClient.GetStreamAsync($"{Program.HassApiEndpoint}/");

            return await JsonSerializer.DeserializeAsync<HomeAssistantApiStatus>(await streamTask);
        }

        public static async Task<List<HomeAssistantState>> GetStates()
        {
            SetupHttpClient();
            
            // Get all the entity states.
            var streamTask = HttpClient.GetStreamAsync($"{Program.HassApiEndpoint}/states");

            return await JsonSerializer.DeserializeAsync<List<HomeAssistantState>>(await streamTask);
        }
        
        public static async Task<HomeAssistantState> GetState(string entityId)
        {
            SetupHttpClient();
            
            // Get one entity state.
            var streamTask = HttpClient.GetStreamAsync($"{Program.HassApiEndpoint}/states/{entityId}");

            return await JsonSerializer.DeserializeAsync<HomeAssistantState>(await streamTask);
        }
        
        public static async Task<List<List<HomeAssistantState>>> GetStateHistory()
        {
            SetupHttpClient();

            // Get all historical states (last 24 hours).
            var timeStamp = DateTime.Now;
            timeStamp = timeStamp.AddHours(-timeStamp.Hour);
            timeStamp = timeStamp.AddMinutes(-timeStamp.Minute);
            timeStamp = timeStamp.AddSeconds(-timeStamp.Second);
            timeStamp = timeStamp.AddMilliseconds(-timeStamp.Millisecond);

            var dateTimeString = timeStamp.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss'Z'");

            var streamTask = HttpClient.GetStreamAsync($"{Program.HassApiEndpoint}/history/period/{dateTimeString}");
            
            return await JsonSerializer.DeserializeAsync<List<List<HomeAssistantState>>>(await streamTask);
        }

        public static async Task<List<List<HomeAssistantState>>> GetStateHistory(string entityId)
        {
            SetupHttpClient();
            
            // Get historical entity states (last 24 hours).
            var timeStamp = DateTime.Now;
            timeStamp = timeStamp.AddHours(-timeStamp.Hour);
            timeStamp = timeStamp.AddMinutes(-timeStamp.Minute);
            timeStamp = timeStamp.AddSeconds(-timeStamp.Second);
            timeStamp = timeStamp.AddMilliseconds(-timeStamp.Millisecond);

            var dateTimeString = timeStamp.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss'Z'");

            var streamTask = HttpClient.GetStreamAsync($"{Program.HassApiEndpoint}/history/period/{dateTimeString}?filter_entity_id={entityId}");
            
            return await JsonSerializer.DeserializeAsync<List<List<HomeAssistantState>>>(await streamTask);
        }
    }
}