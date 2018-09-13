using IdentityModel.OidcClient;
using IdentityModel.OidcClient.Browser;
using OAuthXFDemo.Utils;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using Xamarin.Forms;

namespace OAuthXFDemo.Services
{
    public class OidcAuthenticationService : IAuthenticationService
    {
        private string authorizeUrl = $"{Constants.BaseEndpoint}/connect/authorize";
        private string accesstokenUrl = $"{Constants.BaseEndpoint}/connect/token";

        private OidcClient _client;
        private LoginResult _result;
        private Lazy<HttpClient> _apiClient = new Lazy<HttpClient>(() => new HttpClient());

        public OidcAuthenticationService()
        {
            ConfigureAuthenticator();
        }

        private void ConfigureAuthenticator()
        {
            var browser = DependencyService.Get<IBrowser>();

            var options = new OidcClientOptions
            {
                Authority = "https://demo.identityserver.io",
                ClientId = "native.hybrid",
                Scope = "openid profile email api offline_access",
                RedirectUri = "xamarinformsclients://callback",
                Browser = browser,

                ResponseMode = OidcClientOptions.AuthorizeResponseMode.Redirect
            };

            _client = new OidcClient(options);
        }

        public async void AuthenticateUser()
        {
            _result = await _client.LoginAsync(new LoginRequest());

            if (_result.IsError)
            {                
                return;
            }
        }
        
    }
}
