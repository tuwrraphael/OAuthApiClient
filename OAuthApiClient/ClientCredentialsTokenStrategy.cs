using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Security.Authentication;
using System.Threading.Tasks;

namespace OAuthApiClient
{
    public class ClientCredentialsTokenStrategy : ITokenStrategy
    {
        private readonly ClientCredentialsConfig clientCredentialsConfig;

        public ClientCredentialsTokenStrategy(ClientCredentialsConfig config)
        {
            clientCredentialsConfig = config;
        }

        public async Task<StoredTokens> GetTokens(StoredTokens expiredTokens)
        {
            var tokenClient = new HttpClient();
            var dict = new Dictionary<string, string>() {
                        { "client_id", clientCredentialsConfig.ClientId},
                        { "scope", clientCredentialsConfig.Scopes},
                        {"grant_type", "client_credentials" },
                        {"client_secret", clientCredentialsConfig.ClientSecret }
                    };
            var result = await tokenClient.PostAsync(new Uri(clientCredentialsConfig.ServiceIdentityBaseUrl, "connect/token"),
                new FormUrlEncodedContent(dict));
            if (!result.IsSuccessStatusCode)
            {
                throw new AuthenticationException($"Could not aquire token via client credentials. {result.StatusCode}");
            }
            var tokenResponse = await result.Content.ReadAsStringAsync();
            var res = JsonConvert.DeserializeObject<TokenResponse>(tokenResponse);
            return new StoredTokens()
            {
                AccessToken = res.access_token,
                AccesTokenExpires = DateTime.Now.AddSeconds(res.expires_in)
            };
        }
    }
}
