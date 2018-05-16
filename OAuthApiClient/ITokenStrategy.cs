using System.Threading.Tasks;

namespace OAuthApiClient
{
    public interface ITokenStrategy
    {
        Task<StoredTokens> GetTokens(StoredTokens expiredTokens);
    }
}