using Microsoft.Extensions.Options;
using Osiris.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Osiris.DogApi
{
    public interface IDogsApiCacheWarmer: ICacheWarmer
    {
        IOptions<DogApiSettings> Settings { get; set; }

        Task<bool> CachePirorityBreeds(List<int> priorityList);

        Task<bool> CachePirorityImages(List<int> priorityList);

        Task<bool> CacheAlBreeds();

        Task<bool> CacheAllImages();


    }
}
