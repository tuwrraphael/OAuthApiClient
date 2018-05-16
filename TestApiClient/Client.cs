using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using OAuthApiClient;
using System;

namespace TestApiClient
{
    public interface IClient
    {
    }

    public class Client : IClient
    {
        public Client(IAuthenticationProvider provider, IOptions<ClientOptions> options)
        {

        }
    }

    public class ClientOptions
    {
        public Uri BaseUri { get; set; }
    }

    public static class ClientServiceCollectionExtension
    {
        public static void AddClient(this IServiceCollection services,
            Uri baseUri,
            IAuthenticationProviderBuilder authenticationProviderBuilder)
        {
            var factory = authenticationProviderBuilder.GetFactory();
            services.Configure<ClientOptions>(v => v.BaseUri = baseUri);
            services.AddTransient<IClient>(v => new Client(factory(v), v.GetService<IOptions<ClientOptions>>()));
        }
    }
}
