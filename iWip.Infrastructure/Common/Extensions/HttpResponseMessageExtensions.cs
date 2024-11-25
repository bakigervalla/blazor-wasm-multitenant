/*****************************************************************************

* Copyright (c) 2024 iRobot Corporation. All Rights Reserved.
*****************************************************************************/

using Newtonsoft.Json;

public static class HttpResponseMessageExtensions
{
    public static async Task<T> DeserializeContentAsync<T>(this HttpResponseMessage response) where T : class
    {
        if (!response.IsSuccessStatusCode)
        {
            return default;
        }

        string jsonContent = await response.Content.ReadAsStringAsync();
        T result = JsonConvert.DeserializeObject<T>(jsonContent);
        return result;
    }
}
