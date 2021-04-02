using System;
using System.IO;
using System.Threading.Tasks;

#nullable enable

namespace project_wsa_1
{
    class Program
    {
        public static string? HassApiKey = null;
        public static string? HassApiEndpoint = null;

        public static async Task Main(string[] args)
        {
            // Get the Home Assistant API key from the environment.
            // If it is not passed, throw an IO exception and halt further execution.
            HassApiKey = Environment.GetEnvironmentVariable("HASS_API_KEY");
            if (HassApiKey == null) throw new IOException();

            // Get the Home Assistant API endpoint from the environment.
            // If it is not passed, throw an IO exception and halt further execution.
            HassApiEndpoint = Environment.GetEnvironmentVariable("HASS_API_ENDPOINT");
            if (HassApiEndpoint == null) throw new IOException();

            var entityId = Environment.GetEnvironmentVariable("HASS_ENTITY_ID");
            if (entityId == null) throw new IOException();

            // Print out the gathered variables.
            // Console.WriteLine("Home Assistant configuration:");
            // Console.WriteLine($"\tAPI key: {HassApiKey}");
            // Console.WriteLine($"\tAPI endpoint: {HassApiEndpoint}");
            
            Console.WriteLine("Program started successfully.");

            var apiState = await WebRequest.GetApiState();
            Console.WriteLine($"Home Assistant API state:\n\t{apiState.Message}");

            // Get all the states of the last 24 hours.
            var states = await WebRequest.GetStateHistory(entityId);

            // Calculate the total on-time.
            var totalTimeOn = new TimeSpan();
            var lastOn = false;
            var lastTime = DateTime.Now;
            foreach (var state in states[0])
            {
                // Console.WriteLine(state.EntityId);
                // Console.WriteLine($"\tLast change: {state.LastChanged}");
                // Console.WriteLine($"\tState: {state.State}");
                // Console.WriteLine();

                if (lastOn)
                {
                    totalTimeOn = totalTimeOn.Add(state.LastChangedUTC.Subtract(lastTime));
                }

                lastTime = state.LastChangedUTC;
                lastOn = state.State == "on";
            }
            
            Console.WriteLine($"Your light has been on for {totalTimeOn.Hours} hour(s) and {totalTimeOn.Minutes} minute(s) today.");
        }
    }
}
