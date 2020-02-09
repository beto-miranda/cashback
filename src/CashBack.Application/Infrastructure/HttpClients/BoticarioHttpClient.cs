using System;
using System.Net.Http;

namespace CashBack.Application.Infrastructure.HttpClients
{
    public class BoticarioHttpClient
    {
        private readonly HttpClient httpClient;

        public HttpClient Instance => httpClient;

        public BoticarioHttpClient(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }
    }
}
