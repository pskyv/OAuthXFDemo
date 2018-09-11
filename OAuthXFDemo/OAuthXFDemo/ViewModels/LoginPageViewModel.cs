using OAuthXFDemo.Services;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OAuthXFDemo.ViewModels
{
	public class LoginPageViewModel : BindableBase
	{
        private readonly IAuthenticationService _authenticationService;

        public LoginPageViewModel(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        public DelegateCommand AuthenticateUserCommand => new DelegateCommand(AuthenticateUser);

        private void AuthenticateUser()
        {
            _authenticationService.AuthenticateUser();
        }
    }
}
