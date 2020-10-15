using Osiris.DogApi;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Osiris.Interfaces
{
    public interface IHttpRestClient
    {
        Task<ApiResult<T>> GetAsync<T>(string url);
    }
}
