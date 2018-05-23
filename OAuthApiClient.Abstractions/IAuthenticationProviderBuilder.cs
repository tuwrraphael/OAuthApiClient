#if  NETSTANDARD2_0
using System;

namespace OAuthApiClient.Abstractions
{

    public interface IAuthenticationProviderBuilder
    {
        Func<IServiceProvider, IAuthenticationProvider> GetFactory();
    }
}
#endif
