using System;

namespace OAuthApiClient.Abstractions
{
    public interface IAuthenticationProviderBuilder
    {
        Func<IServiceProvider, IAuthenticationProvider> GetFactory();
    }
}
