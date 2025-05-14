namespace EntegroConnect.Domain.Services;

public abstract class BaseService
{
    private readonly HttpClient _httpClient;

    public BaseService(IHttpClientFactory httpClientFactory, string httpClientName)
    {
        _httpClient = httpClientFactory.CreateClient(httpClientName);
    }

    internal async Task<JObject> GetAsync(string url)
    {
        var result = await _httpClient.GetAsync(url);
        if (!result.IsSuccessStatusCode)
            throw new HttpRequestException(result.ReasonPhrase);

        using var reader = new StringReader(await result.Content.ReadAsStringAsync());
        var responseString = await result.Content.ReadAsStringAsync();
        return JObject.Parse(responseString);
    }

    internal async Task<JObject> PostAsync(string url, object data)
    {
        var json = JsonConvert.SerializeObject(data);
        var content = new StringContent(json, Encoding.UTF8, "application/json");
        var result = await _httpClient.PostAsync(url, content);

        // Trey: Do not throw exception, return result so the error can be parsed by the service calling this method
        //if (!result.IsSuccessStatusCode)
        //    throw new HttpRequestException(result.ReasonPhrase);

        using var reader = new StringReader(await result.Content.ReadAsStringAsync());
        var responseString = await result.Content.ReadAsStringAsync();
        return JObject.Parse(responseString);
    }

    internal async Task<JObject> PutAsync(string url, object data)
    {
        var json = JsonConvert.SerializeObject(data);
        var content = new StringContent(json, Encoding.UTF8, "application/json");
        var result = await _httpClient.PutAsync(url, content);
        if (!result.IsSuccessStatusCode)
            throw new HttpRequestException(result.ReasonPhrase);

        using var reader = new StringReader(await result.Content.ReadAsStringAsync());
        var responseString = await result.Content.ReadAsStringAsync();
        return JObject.Parse(responseString);
    }
}