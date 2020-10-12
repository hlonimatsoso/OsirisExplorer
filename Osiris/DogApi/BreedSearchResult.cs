using System;
using System.Collections.Generic;
using System.Text;

namespace Osiris.DogApi
{
    public class BreedSearchResult : Breed
    {
        public string breed_group { get; set; }

        public string bred_for { get; set; }
    }
}
