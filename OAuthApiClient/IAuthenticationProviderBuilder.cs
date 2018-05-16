using System;

namespace OAuthApiClient
{
    public interface IAuthenticationProviderBuilder
    {
        Func<IServiceProvider, IAuthenticationProvider> GetFactory();
    }
}
