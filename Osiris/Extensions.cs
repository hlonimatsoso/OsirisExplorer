using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Options;
using Osiris.DogApi;
using Osiris.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Osiris
{
    public static class Extensions
    {
        public static void EvaluateCacheSettings(this IApplicationBuilder app)
        {
            var settings = app.ApplicationServices.GetService(typeof(IOptions<DogApiSettings>)) as IOptions<DogApiSettings>;
            var cacheWarmer = app.ApplicationServices.GetService(typeof(IOsirisCacheWarmer)) as IOsirisCacheWarmer;

            cacheWarmer.WarmAllCache();
        }
    }
}
