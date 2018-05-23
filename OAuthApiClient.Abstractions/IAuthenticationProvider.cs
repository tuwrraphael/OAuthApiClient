using System.Net.Http;
using System.Threading.Tasks;

namespace OAuthApiClient.Abstractions
{
    public interface IAuthenticationProvider
    {
        Task AuthenticateClient(HttpClient client);
    }
}
