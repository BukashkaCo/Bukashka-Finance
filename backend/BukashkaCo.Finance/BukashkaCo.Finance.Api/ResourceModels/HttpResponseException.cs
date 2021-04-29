using System;

namespace BukashkaCo.Finance.Api.ResourceModels
{
    public class HttpResponseException : Exception
    {
        public int StatusCode { get; set; }
        public Error Error { get; set; }
    }
}