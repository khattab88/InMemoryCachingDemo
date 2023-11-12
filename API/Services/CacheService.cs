using System.Runtime.Caching;

namespace API.Services
{
    public class CacheService : ICacheService
    {
        private ObjectCache _memoryCache = MemoryCache.Default;

        public T GetData<T>(string key)
        {
            try
            {
                T item = (T) _memoryCache.Get(key);
                return item;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public object RemoveData(string key)
        {
            bool result = true;

            try
            {
                if (!string.IsNullOrEmpty(key))
                    _memoryCache.Remove(key);
                else
                    result = false;

                return result;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public bool SetData<T>(string key, T value, DateTimeOffset expirationTime)
        {
            bool result = true;

            try
            {
                if (!string.IsNullOrEmpty(key))
                    _memoryCache.Set(key, value, expirationTime);                
                else
                    result = false;

                return result;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
