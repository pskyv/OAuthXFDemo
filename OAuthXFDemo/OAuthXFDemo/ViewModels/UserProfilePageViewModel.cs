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
        private bool _isLoading;

        public UserProfilePageViewModel(IApiService apiService)
        {
            _apiService = apiService;
        }

        public ApplicationUser User
        {
            get { return _user; }
            set { SetProperty(ref _user, value); }
        }

        public bool IsLoading
        {
            get { return _isLoading; }
            set { SetProperty(ref _isLoading, value); }
        }        


        public async void OnNavigatingTo(NavigationParameters parameters)
        {
            IsLoading = true;
            try
            {
                User = await _apiService.GetUserInfo();
            }
            catch(Exception e)
            {

            }
            finally
            {
                IsLoading = false;
            }
        }
    }
}
