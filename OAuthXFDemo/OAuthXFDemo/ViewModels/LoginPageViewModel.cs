using OAuthXFDemo.Models;
using OAuthXFDemo.Services;
using OAuthXFDemo.Utils;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Essentials;

namespace OAuthXFDemo.ViewModels
{
	public class LoginPageViewModel : BindableBase
	{
        private readonly IAuthenticationService _authenticationService;
        private readonly INavigationService _navigationService;
        private bool _hasConnection;
        private bool _tokenExists;

        public LoginPageViewModel(IAuthenticationService authenticationService, INavigationService navigationService)
        {
            _authenticationService = authenticationService;
            _navigationService = navigationService;

            TokenExists = Preferences.ContainsKey("ExpiryDate");
            HasConnection = !(Connectivity.NetworkAccess == NetworkAccess.None);
            Connectivity.ConnectivityChanged += OnConnectivityChanged;
        }

        public bool HasConnection
        {
            get { return _hasConnection; }
            set { SetProperty(ref _hasConnection, value); }
        }

        public DateTime TokenExpires
        {
            get { return Preferences.Get("ExpiryDate", DateTime.Now); }
        }

        public bool TokenExists
        {
            get { return _tokenExists; }
            set { SetProperty(ref _tokenExists, value); }
        }

        private void OnConnectivityChanged(object sender, ConnectivityChangedEventArgs e)
        {
            HasConnection = !(e.NetworkAccess == NetworkAccess.None);
            if(!HasConnection)
            {
                HelperFunctions.ShowToastMessage(ToastMessageType.Error, "There's no network connection");
            }
        }        

        public DelegateCommand AuthenticateUserCommand => new DelegateCommand(AuthenticateUser).ObservesCanExecute(() => HasConnection);

        private async void AuthenticateUser()
        {
            if (Preferences.Get("IsLoggedIn", false) && DateTime.Now < Preferences.Get("ExpiryDate", DateTime.Now))
            {
                await _navigationService.NavigateAsync("NavigationPage/UserProfilePage");
            }

            else
            {

                try
                {
                    Preferences.Set("IsLoggedIn", false);
                    _authenticationService.AuthenticateUser();
                }
                catch (Exception e)
                {

                }
            }
        }
    }
}
