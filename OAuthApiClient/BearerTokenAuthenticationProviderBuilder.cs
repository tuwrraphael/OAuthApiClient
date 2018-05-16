using Microsoft.Extensions.DependencyInjection;
using System;

namespace OAuthApiClient
{
    public class BearerTokenAuthenticationProviderBuilder : IAuthenticationProviderBuilder
    {
        private readonly IServiceCollection services;
        private readonly string tokenIdentifier;
        private Func<IServiceProvider, IAuthenticationProvider> factory;

        public BearerTokenAuthenticationProviderBuilder(IServiceCollection serviceCollection, string tokenIdentifier)
        {
            services = serviceCollection;
            this.tokenIdentifier = tokenIdentifier;
        }

        public void UseMemoryCacheTokenStore()
        {
            services.AddTransient<ITokenStore, MemoryCacheTokenStore>();
        }

        public void UseClientCredentialsTokenStrategy(ClientCredentialsConfig clientCredentialsConfig)
        {
            factory = svc => new BearerTokenAuthenticationProvider(svc.GetService<ITokenStore>(), new ClientCredentialsTokenStrategy(clientCredentialsConfig),
                tokenIdentifier);
        }

        public Func<IServiceProvider, IAuthenticationProvider> GetFactory() => factory;
    }
}
