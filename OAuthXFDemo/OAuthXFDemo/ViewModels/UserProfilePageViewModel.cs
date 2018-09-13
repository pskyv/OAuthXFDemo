using OAuthXFDemo.Models;
using OAuthXFDemo.Services;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OAuthXFDemo.ViewModels
{
	public class UserProfilePageViewModel : BindableBase, INavigatingAware
	{
        private readonly IApiService _apiService;
        private ApplicationUser _user;

        public UserProfilePageViewModel(IApiService apiService)
        {
            _apiService = apiService;
        }

        public ApplicationUser User
        {
            get { return _user; }
            set { SetProperty(ref _user, value); }
        }

        public async void OnNavigatingTo(NavigationParameters parameters)
        {
            User = await _apiService.GetUserInfo();
        }
    }
}
