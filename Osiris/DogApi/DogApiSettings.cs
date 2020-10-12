using System;
using System.Collections.Generic;
using System.Text;

namespace Osiris.DogApi
{
    public class DogApiSettings
    {
        public string baseUrl { get; set; }

        public string key { get; set; }

        public string queries_breeds { get; set; }

        public string queries_categories { get; set; }

        public string queries_images { get; set; }

    }

    public class DogApiQueries
    {

        public string breeds { get; set; }
        public string categories { get; set; }

    }
}
