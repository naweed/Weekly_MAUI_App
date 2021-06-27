namespace Weekly_MAUI_App.Framework.Services
{
    public class RestServiceBase
    {
        private HttpClient _httpClient;

        protected RestServiceBase(string apiBaseUrl)
        {
            _httpClient = new HttpClient()
            {
                BaseAddress = new Uri(apiBaseUrl)
            };

            _httpClient.DefaultRequestHeaders.Accept.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

        }

        protected async Task<T> GetAsync<T>(string resource, NetworkAccess networkAccess, int cacheDuration = 24)
        {
            //Get Json data (from Cache or Web)
            var json = await GetJsonAsync(resource, networkAccess, cacheDuration);

            //Return the result
            return JsonSerializer.Deserialize<T>(json);

        }

        private async Task<string> GetJsonAsync(string resource, NetworkAccess networkAccess, int cacheDurationInHours = 24)
        {
            var cleanCacheKey = resource.CleanCacheKey();

            //Try Get data from Cache
            var cachedData = CacheService.Get<string>(cleanCacheKey);

            if (cacheDurationInHours > 0 && cachedData is not null)
            {
                //If the cached data is still valid
                if (!CacheService.IsExpired(cleanCacheKey))
                    return cachedData;
            }

            //Check for internet connection and return cached data if possible
            if (networkAccess != NetworkAccess.Internet)
            {
                if (cachedData is not null)
                {
                    return cachedData;
                }
                else
                {
                    throw new InternetConnectionException();
                }
            }

            //No Cache Found, or Cached data was not required, or Internet connection is also available
            //Extract response from URI
            var response = await _httpClient.GetAsync(new Uri(_httpClient.BaseAddress, resource));

            response.EnsureSuccessStatusCode();

            //Read Response
            string json = await response.Content.ReadAsStringAsync();

            //Save to Cache if required
            if (cacheDurationInHours > 0)
            {
                try
                {
                    CacheService.Add(cleanCacheKey, json, TimeSpan.FromHours(cacheDurationInHours));
                }
                catch { }
            }

            //Return the result
            return json;

        }
    }
}
