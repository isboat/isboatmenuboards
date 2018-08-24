using System;
using System.Web;
using System.Web.Caching;
using MenuBoards.Interfaces.Web;

namespace MenuBoards.Services
{
    public class SessionService: ISessionService
    {
        public bool Contains(string key)
        {
            return HttpRuntime.Cache[key] != null;
        }

        public T Get<T>(string key)
        {
            var result = HttpRuntime.Cache[key];

            if (result is T)
            {
                return (T)result;
            }

            // else return the default value of t
            return default(T);
        }

        public void Remove(string key)
        {
            HttpRuntime.Cache.Remove(key);
        }

        public void Set(string key, object obj)
        {
            HttpRuntime.Cache.Add(key, obj, null, DateTime.Now.AddMinutes(60), System.Web.Caching.Cache.NoSlidingExpiration, CacheItemPriority.Default, null);
        }
    }
}