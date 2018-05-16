using System;

namespace OAuthApiClient
{
    public class ClientCredentialsConfig
    {
        public string ClientId { get; set; }
        public string ClientSecret { get; set; }
        public Uri ServiceIdentityBaseUrl { get; set; }
        public string Scopes { get; set; }
    }
}
