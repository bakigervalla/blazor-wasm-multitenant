/*****************************************************************************

* Copyright (c) 2024 iRobot Corporation. All Rights Reserved.
*****************************************************************************/

using iWip.Infrastructure.Models.Users;
using Microsoft.AspNetCore.Components;
using System.Net;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace iWip.Infrastructure.Services.Http;

public class HttpService : IHttpService
{
    private HttpClient _httpClient;
    private NavigationManager _navigationManager;
    private ILocalStorageService _localStorageService;

    public HttpService(
        HttpClient httpClient,
        IHttpClientFactory factory,
        NavigationManager navigationManager,
        ILocalStorageService localStorageService
    )
    {
        _httpClient = httpClient;
        _httpClient = factory.CreateClient("iwipcloud");
        _navigationManager = navigationManager;
        _localStorageService = localStorageService;
    }

    public async Task<HttpResponseMessage> GetAsync(string uri)
    {
        var request = new HttpRequestMessage(HttpMethod.Get, uri);
        return await sendRequest(request);
    }

    public async Task<HttpResponseMessage> PostAsync(string uri, object value)
    {
        var request = new HttpRequestMessage(HttpMethod.Post, uri);
        var res = JsonSerializer.Serialize(value);
        request.Content = new StringContent(JsonSerializer.Serialize(value), Encoding.UTF8, "application/json");
        return await sendRequest(request);
    }

    public async Task<HttpResponseMessage> PutAsync(string uri, object value)
    {
        var request = new HttpRequestMessage(HttpMethod.Put, uri);
        request.Content = new StringContent(JsonSerializer.Serialize(value), Encoding.UTF8, "application/json");
        return await sendRequest(request);
    }

    public async Task<HttpResponseMessage> DeleteAsync(string uri)
    {
        var request = new HttpRequestMessage(HttpMethod.Delete, uri);
        return await sendRequest(request);
    }


    // helper methods

    private async Task<HttpResponseMessage> sendRequest(HttpRequestMessage request)
    {
        string _token = await _localStorageService.GetItem<string>("iwipcloud_user");
        User user = null;

        if (!string.IsNullOrEmpty(_token))
            user = Newtonsoft.Json.JsonConvert.DeserializeObject<User>(_token)!;

        var isApiUrl = !request.RequestUri.IsAbsoluteUri;
        if (user != null && isApiUrl)
        {
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", user.AccessToken);

            if (user.MANUFACTURER.HasValue)
                request.Headers.Add("tenantId", user.MANUFACTURER.ToString());

            request.Headers.Add("userId", user.Id.ToString());
        }

        var response = await _httpClient.SendAsync(request);

        // auto logout on 401 response
        if (response.StatusCode == HttpStatusCode.Unauthorized && !request.RequestUri.Segments.Any(x => x == "signin"))
        {
            _navigationManager.NavigateTo("authentication/unauthorized");
            return response;
        }

        return response;
    }
}
