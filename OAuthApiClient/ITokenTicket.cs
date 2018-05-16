using System;
using System.Threading.Tasks;

namespace OAuthApiClient
{
    public interface ITokenTicket : IDisposable
    {
        StoredTokens Get();
        Task Update(StoredTokens tokens);
    }
}
