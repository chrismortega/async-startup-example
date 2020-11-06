using System.Threading.Tasks;

namespace AsyncStartupExample.Api.Interfaces
{
    public interface ISecretKeyValueStore
    {
        Task<string> GetValueAsync(string key);
    }
}