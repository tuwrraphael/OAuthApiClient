using System.Threading.Tasks;

namespace OAuthApiClient
{
    public interface ITokenStore
    {
        Task<ITokenTicket> Get(string name);
    }
}
