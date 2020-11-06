using AsyncStartupExample.Api.Interfaces;
using System.Net.Http;

namespace AsyncStartupExample.Api.Infrastructure
{
    public class MyProxyService : IMyProxyService
    {
        private readonly HttpClient _client;

        public MyProxyService(HttpClient client) => _client = client;

        // todo: expose methods that call some api that requires basic auth
    }
}