using System;
using System.Net.Http;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Text;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Osiris.Interfaces;
using Osiris.BlazorApp.Clients;
using Microsoft.Extensions.Caching.Memory;

namespace Osiris.BlazorApp
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("app");

            builder.Services.AddHttpClient("api", c =>
            {
                c.BaseAddress = new Uri("https://localhost:62222/");
            });


            //builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://localhost:62222/") });

            builder.Services.AddScoped<IDogsApiClient, DogsApiClient>();

            builder.Services.AddScoped<IConfigClient, ConfigClient>();

            builder.Services.AddScoped<IHttpRestClient, HttpRestClient>();

            builder.Services.AddScoped<ICacheIsKing, OsirisCache>();

            builder.Services.AddScoped<IMemoryCache, MemoryCache>();


            await builder.Build().RunAsync();
        }
    }
}
