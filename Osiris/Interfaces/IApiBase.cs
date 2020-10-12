using Osiris.DogApi;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Osiris.Interfaces
{
    public interface IApiBase
    {
        HttpClient Client { get; set; }

        Task<ApiResult<T>> Get<T>(string url);

        ApiResult<bool> Post<T>(string url);

    }
}
