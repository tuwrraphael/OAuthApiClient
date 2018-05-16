using Microsoft.Extensions.DependencyInjection;

namespace OAuthApiClient
{
    public static class AuthenticationProviderServiceCollectionExtension
    {
        public static BearerTokenAuthenticationProviderBuilder AddBearerTokenAuthenticationProvider(this IServiceCollection services, string tokenIdentifier)
        {
            return new BearerTokenAuthenticationProviderBuilder(services, tokenIdentifier);
        }
    }
}
