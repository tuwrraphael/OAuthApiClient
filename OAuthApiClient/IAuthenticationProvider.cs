using System.Net.Http;
using System.Threading.Tasks;

namespace OAuthApiClient
{
    public interface IAuthenticationProvider<TTypedClient>
    {
        Task AuthenticateClient(HttpClient client);
    }
}
