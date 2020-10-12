using System;
using System.Collections.Generic;
using System.Text;

namespace Osiris.DogApi
{
    public class Breed
    {
        public string id { get; set; }

        public string name { get; set; }

        public string breed_group { get; set; }

        public string bred_for { get; set; }

        public string temprament { get; set; }

        public string life_span { get; set; }

        public string alt_names { get; set; }

        public string wikipedia_url { get; set; }

        public string origin { get; set; }

        public string country_code { get; set; }

        public HeightAndWeight weight { get; set; }

        public HeightAndWeight height { get; set; }


    }
}
