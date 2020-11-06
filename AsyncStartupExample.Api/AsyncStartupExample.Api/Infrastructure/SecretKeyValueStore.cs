using AsyncStartupExample.Api.Interfaces;
using System.Threading.Tasks;

namespace AsyncStartupExample.Api.Infrastructure
{
    public class SecretKeyValueStore : ISecretKeyValueStore
    {
        public async Task<string> GetValueAsync(string key) =>
            await Task.FromResult("this is a very secret password which would normally have been retrieved from a third party!");
    }
}
