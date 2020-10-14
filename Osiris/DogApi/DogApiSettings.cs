using System;
using System.Collections.Generic;
using System.Text;

namespace Osiris.DogApi
{
    public class DogApiSettings
    {
        public string baseUrl { get; set; }

        public string key { get; set; }

        public DogApiUrls<string> urls { get; set; }

        public DogApiCacheStrategies cache_stratergies { get; set; }


    }

    public class DogApiUrls<T>
    {

        public T breeds { get; set; }
        public T categories { get; set; }
        public T images { get; set; }

    }

    public class DogApiCacheStrategies : DogApiUrls<int>
    {


    }
}
