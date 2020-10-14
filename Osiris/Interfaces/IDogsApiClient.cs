using Osiris.DogApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Osiris.Interfaces
{
    public interface IDogsApiClient : IDogsClient
    {
        List<Breed> Breeds { get; set; }

        List<Image> Dogs { get; set; }

    }
}
