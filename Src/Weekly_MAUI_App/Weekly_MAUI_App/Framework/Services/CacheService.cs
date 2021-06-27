namespace Weekly_MAUI_App.Framework.Services
{
    public static class CacheService
    {
        ////TODO: Replace with persistent logic and make use of CacheTimeOut
        

        private static Dictionary<string, object> _localCaches = new();

        public static bool Exists(string cacheKey) => _localCaches.ContainsKey(cacheKey);

        public static bool IsExpired(string cacheKey) => false; //TODO: Handle the TimeOut

        public static T Get<T>(string cacheKey) => (_localCaches.ContainsKey(cacheKey)? (T)_localCaches[cacheKey] : default(T));

        public static void Add(string cacheKey, object cacheObject, TimeSpan timeToCache) //TODO: Handle the TimeOut
        {
            if (_localCaches.ContainsKey(cacheKey))
                _localCaches[cacheKey] = cacheObject;
            else
                _localCaches.Add(cacheKey, cacheObject);
        }
    }
}
