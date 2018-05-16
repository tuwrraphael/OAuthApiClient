using System;

namespace OAuthApiClient
{
    public class StoredTokens
    {
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
        public DateTime? AccesTokenExpires { get; set; }

        public bool HasValidAccessToken => null != AccessToken && AccesTokenExpires.HasValue && AccesTokenExpires > DateTime.Now;
    }
}
