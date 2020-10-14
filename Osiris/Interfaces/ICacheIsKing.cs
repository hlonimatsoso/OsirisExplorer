using System;
using System.Collections.Generic;
using System.Text;

namespace Osiris.Interfaces
{
    public interface ICacheIsKing
    {
        bool TryGetCache<T>(string key, out T cache);
        
        T SetCache<T>(string key, T @object, int cacheDurationInSeconds);

        bool RunCacheWarmer();
    }
}
