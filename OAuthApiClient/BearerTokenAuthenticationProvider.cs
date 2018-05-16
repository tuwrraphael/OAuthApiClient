using System.Net.Http;
using System.Threading.Tasks;

namespace OAuthApiClient
{
    public class BearerTokenAuthenticationProvider<TTypedClient> : IAuthenticationProvider<TTypedClient>
    {
        private readonly ITokenStore tokenStore;
        private readonly ITokenStrategy tokenRenewalStrategy;

        public BearerTokenAuthenticationProvider(
            ITokenStore tokenStore, ITokenStrategy tokenRenewalStrategy)
        {
            this.tokenStore = tokenStore;
            this.tokenRenewalStrategy = tokenRenewalStrategy;
        }

        public async Task AuthenticateClient(HttpClient client)
        {
            using (var tokenTicket = await tokenStore.Get(typeof(TTypedClient).GetType().Name))
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
