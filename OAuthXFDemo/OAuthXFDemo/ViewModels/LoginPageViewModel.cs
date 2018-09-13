using OAuthXFDemo.Models;
using OAuthXFDemo.Services;
using OAuthXFDemo.Utils;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Essentials;

namespace OAuthXFDemo.ViewModels
{
	public class LoginPageViewModel : BindableBase
	{
        private readonly IAuthenticationService _authenticationService;
        private bool _hasConnection;

        public LoginPageViewModel(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
            Preferences.Set("IsLoggedIn", false);
            HasConnection = !(Connectivity.NetworkAccess == NetworkAccess.None);
            Connectivity.ConnectivityChanged += OnConnectivityChanged;
        }

        private void OnConnectivityChanged(object sender, ConnectivityChangedEventArgs e)
        {
            HasConnection = !(e.NetworkAccess == NetworkAccess.None);
            if(!HasConnection)
            {
                HelperFunctions.ShowToastMessage(ToastMessageType.Error, "There's no network connection");
            }
        }

        public bool HasConnection
        {
            get { return _hasConnection; }
            set { SetProperty(ref _hasConnection, value); }
        }

        public DelegateCommand AuthenticateUserCommand => new DelegateCommand(AuthenticateUser).ObservesCanExecute(() => HasConnection);        

        private void AuthenticateUser()
        {
            try
            {
                _authenticationService.AuthenticateUser();
            }
            catch(Exception e)
            {

            }
        }
    }
}
