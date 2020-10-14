using System;
using System.Collections.Generic;
using System.Text;

namespace Osiris.DogApi
{
    public class DogApiSettings
    {
        public string baseUrl { get; set; }

        public string key { get; set; }

        public DogApiUrls urls { get; set; }

        public DogApiCacheStrategies cache_stratergies { get; set; }

        public CacheWarmers cache_warmers { get; set; }
    }

    public abstract class DogApi<T>
    {

        public T breeds { get; set; }
        public T categories { get; set; }
        public T images { get; set; }

    }

    public class DogApiUrls : DogApi<string>
    {


    }

    public class DogApiCacheStrategies : DogApi<int>
    {


    }

    public class CacheWarmers
    {
        public List<int> breeds { get; set; }

        public List<int> images { get; set; }

        public bool all_breeds { get; set; }

        public bool all_images { get; set; }

    }
}
