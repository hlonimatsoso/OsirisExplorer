using System;
using System.Collections.Generic;
using System.Text;

namespace Osiris.DogApi
{
    public class Image
    {
        public string id { get; set; }

        public string url { get; set; }

        public List<Category> categories { get; set; }

        public List<Breed> breeds { get; set; }

        public int width { get; set; }

        public int height { get; set; }

    }
}
