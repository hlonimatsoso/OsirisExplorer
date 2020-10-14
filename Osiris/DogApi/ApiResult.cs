using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace Osiris.DogApi
{
    public class ApiResult<T>
    {
        public HttpStatusCode HttpStatusCode { get; set; }

        public bool IsValid { get; set; }

        public T Results { get; set; }

        public string ErrorMessage { get; set; }

        public bool IsCachedData { get; set; }

    }
}
