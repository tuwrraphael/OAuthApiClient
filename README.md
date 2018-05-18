#  OAuthApiClient
This package helps to implement http api clients for apis secured using bearer token authentication.
The package especially targets .NET Core 2.0 Web Projects.

A possible client implementation looks like this:
~~~c#
public interface IClient
{
    Task<string> GetValueAsync();
}

public class Client : IClient
{
    private readonly IAuthenticationProvider provider;
    public Client(IAuthenticationProvider provider)
    {
        this.provider = provider;
    }
    public async Task<string> GetValueAsync() {
        var client = new HttpClient();
        await provider.AuthenticateClient(client);
        var response = await client.GetAsync("http://url.com/api/Value");
        ...
    }
}
~~~

The for the IAuthenticationProvider currently the following strategies are supported:
* BearerTokenAuthenticationProvider, using ClientCredentialsTokenStrategy and MemoryCacheTokenStore

The client could be then configures as (Startup.cs)
~~~c#
public static class ClientServiceCollectionExtension
{
    public static void AddClient(this IServiceCollection services,
        IAuthenticationProviderBuilder authenticationProviderBuilder)
    {
        var factory = authenticationProviderBuilder.GetFactory();
        services.AddTransient<IClient>(v => new Client(factory(v)));
    }
}
//Startup.cs
public void ConfigureServices(IServiceCollection services)
{
    var authenticationProviderBuilder = services.AddBearerTokenAuthenticationProvider("clientBearerToken")
      .UseMemoryCacheTokenStore()
      .UseClientCredentialsTokenStrategy(new ClientCredentialsConfig() {
                      ClientId = "ClientId",
                      ClientSecret = "Secret",
                      Scopes = "scope1 scope2",
                      ServiceIdentityBaseUrl = new Uri("https://identityserver.url")
                  });
    services.AddClient(authenticationProviderBuilder);
    // add another client using the same token
    services.AddClient2(authenticationProviderBuilder);
}
~~~

Currently supported token strategies:
* ClientCredentials
Currently supported token storing/caching methods:
* MemoryCache
