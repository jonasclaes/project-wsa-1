# Project Windows System Administration

The purpose of this project is to create a .NET Core application.
I have chosen to count the time that any light in my house has been on since midnight.
This has been done by using the Home Assistant API together with C# and the HttpClient.

This program makes use of environment variables.

| Required  | Name              | Description                           |
| --------- | ----------------- | ------------------------------------- |
| Yes       | HASS_API_KEY      | The Home Assistant API key.           |
| Yes       | HASS_API_ENDPOINT | The URL for the Home Assistant API.   |
| Yes       | HASS_ENTITY_ID    | The entity you want to process.       |