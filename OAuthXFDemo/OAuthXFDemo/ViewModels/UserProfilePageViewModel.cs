using OAuthXFDemo.Models;
using OAuthXFDemo.Services;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Essentials;

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

        public DateTime TokenExpires
        {
            get { return Preferences.Get("ExpiryDate", DateTime.Now); }
        }


        public async void OnNavigatingTo(NavigationParameters parameters)
        {
            User = await _apiService.GetUserInfo();
        }
    }
}
