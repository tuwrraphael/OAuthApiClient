using System.Net.Http;
using System.Threading.Tasks;

namespace OAuthApiClient
{
    public class BearerTokenAuthenticationProvider : IAuthenticationProvider
    {
        private readonly ITokenStore tokenStore;
        private readonly ITokenStrategy tokenRenewalStrategy;
        private readonly string tokenIdentifier;

        public BearerTokenAuthenticationProvider(ITokenStore tokenStore, ITokenStrategy tokenRenewalStrategy,
            string tokenIdentifier)
        {
            this.tokenStore = tokenStore;
            this.tokenRenewalStrategy = tokenRenewalStrategy;
            this.tokenIdentifier = tokenIdentifier;
        }

        public async Task AuthenticateClient(HttpClient client)
        {
            using (var tokenTicket = await tokenStore.Get(tokenIdentifier))
            {
                var tokens = tokenTicket.Get();
                if (!tokens.HasValidAccessToken)
                {
                    tokens = await tokenRenewalStrategy.GetTokens(tokens);
                    await tokenTicket.Update(tokens);
                }
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", tokens.AccessToken);
            }
        }
    }
}
