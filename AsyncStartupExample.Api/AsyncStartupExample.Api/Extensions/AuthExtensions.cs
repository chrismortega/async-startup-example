using AsyncStartupExample.Api.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

namespace AsyncStartupExample.Api.Extensions
{
    public static class AuthExtensions
    {
        /// <summary>
        /// Sets up a client with basic auth by grabbing the a password from a hosted key/value store and attaching that to outgoing requests
        /// </summary>
        /// <typeparam name="TClient">The interface type you want to register</typeparam>
        /// <typeparam name="TImplementation">The implementation of the type you are registering</typeparam>
        /// <param name="services">The services collection</param>
        /// <param name="secretKey">The secret key used to lookup the password in the key/value store</param>
        /// <returns>The HttpClientBuilder</returns>
        public static IHttpClientBuilder AddHttpClientWithBasicAuth<TClient, TImplementation>(
            this IServiceCollection services,
            string secretKey)
            where TClient : class
            where TImplementation : class, TClient =>
                services.AddHttpClient<TClient, TImplementation>()
                    .ConfigureHttpClient((provider, client) => ConfigureHttpClientForBasicAuth(provider, client, secretKey));

        private static void ConfigureHttpClientForBasicAuth(IServiceProvider provider, HttpClient client, string secretKey)
        {
            var kvStore = provider.GetRequiredService<ISecretKeyValueStore>();
            string password = kvStore.GetValueAsync(secretKey).GetAwaiter().GetResult();  // <----- async method that we need to call

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", GetBasicAuthToken("TestUser", password));
        }

        private static string GetBasicAuthToken(string username, string password) =>
            Convert.ToBase64String(
                ASCIIEncoding.ASCII.GetBytes(
                    $"{username}:{password}"));
    }
}
