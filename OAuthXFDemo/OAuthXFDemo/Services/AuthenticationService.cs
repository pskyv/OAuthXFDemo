using OAuthXFDemo.Utils;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Auth;

namespace OAuthXFDemo.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        public AuthenticationService()
        {
            //ConfigureAuthenticator();
        }

        private void ConfigureAuthenticator()
        {
            Authenticator = new OAuth2Authenticator(
                Constants.clientId,
                Constants.clientSecret,
                Constants.scope,
                new Uri(Constants.authorizeUrl),
                new Uri(Constants.redirectUri),
                new Uri(Constants.accesstokenUrl),
                null, 
                true
                );

            Authenticator.Completed += OnAuthenticatorCompleted;
            Authenticator.Error += OnAuthenticatorError;

            AuthenticationState.Authenticator = Authenticator;
        }        

        private void OnAuthenticatorCompleted(object sender, AuthenticatorCompletedEventArgs e)
        {
            if (e.IsAuthenticated)
            {
                
            }
        }

        private void OnAuthenticatorError(object sender, AuthenticatorErrorEventArgs e)
        {
            
        }

        public OAuth2Authenticator Authenticator;

        public void AuthenticateUser()
        {
            var presenter = new Xamarin.Auth.Presenters.OAuthLoginPresenter();
            presenter.Login(Authenticator);
        }
    }
}
