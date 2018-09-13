using OAuthXFDemo.Utils;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Auth;
using Xamarin.Forms;

namespace OAuthXFDemo.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly INavigationService _navigationService;
        private string authorizeUrl = $"{Constants.BaseEndpoint}/connect/authorize";
        private string accesstokenUrl = $"{Constants.BaseEndpoint}/connect/token";

        public AuthenticationService(INavigationService navigationService)
        {
            _navigationService = navigationService;
            ConfigureAuthenticator();
        }

        private void ConfigureAuthenticator()
        {
            Authenticator = new OAuth2Authenticator(
                Constants.clientId,
                Constants.clientSecret,
                Constants.scope,
                new Uri(authorizeUrl),
                new Uri(Constants.redirectUri),
                new Uri(accesstokenUrl),
                null, 
                true
                );

            Authenticator.Completed += OnAuthenticatorCompleted;
            Authenticator.Error += OnAuthenticatorError;

            AuthenticationState.Authenticator = Authenticator;
        }        

        private async void OnAuthenticatorCompleted(object sender, AuthenticatorCompletedEventArgs e)
        {
            var authenticator = sender as OAuth2Authenticator;

            if (authenticator != null)
            {
                authenticator.Completed -= OnAuthenticatorCompleted;
                authenticator.Error -= OnAuthenticatorError;
            }

            if (e.IsAuthenticated)
            {                
                var accessToken = e.Account.Properties["access_token"].ToString();
                await NavigateToPage();
                DependencyService.Get<IActivityService>().StartActivity();
            }
        }

        private async Task NavigateToPage()
        {
            await _navigationService.NavigateAsync("MainPage");
        }

        private void OnAuthenticatorError(object sender, AuthenticatorErrorEventArgs e)
        {
            
        }

        public static OAuth2Authenticator Authenticator;

        public void AuthenticateUser()
        {
            try
            {
                var presenter = new Xamarin.Auth.Presenters.OAuthLoginPresenter();
                presenter.Login(Authenticator);
            }
            catch(Exception e)
            {

            }
        }
    }
}
