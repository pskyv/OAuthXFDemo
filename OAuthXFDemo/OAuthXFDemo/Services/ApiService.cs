using Newtonsoft.Json;
using OAuthXFDemo.Models;
using OAuthXFDemo.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Auth;
using Xamarin.Essentials;

namespace OAuthXFDemo.Services
{    
    public class ApiService : IApiService
    {
        private string userInfoUrl = $"{Constants.BaseEndpoint}/connect/userinfo";
        private HttpClient _client;
        private NetworkAccess _networkAccess;
        

        public ApiService()
        {
            ConfigureHttpClient();

            _networkAccess = Connectivity.NetworkAccess;
            Connectivity.ConnectivityChanged += OnConnectivityChanged;
        }

        private void OnConnectivityChanged(object sender, ConnectivityChangedEventArgs e)
        {
            _networkAccess = e.NetworkAccess;   
        }

        private void ConfigureHttpClient()
        {
            _client = new HttpClient();

            var account = AccountStore.Create().FindAccountsForService(Constants.AccessTokenKey).FirstOrDefault();
            if (account != null)
            {                
                _client.SetBearerToken(account.Properties["access_token"].ToString());
                //_client.DefaultRequestHeaders.Add("Authorization", "Bearer " + accessToken);
            }
        }

        public async Task<ApplicationUser> GetUserInfo()
        {
            if(_networkAccess == NetworkAccess.None)
            {
                HelperFunctions.ShowToastMessage(ToastMessageType.Error, "There's no network connection");
                return null;
            }

            ApplicationUser user = null;

            var response = await _client.GetAsync(userInfoUrl);

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var userJson = await response.Content.ReadAsStringAsync();
                user = JsonConvert.DeserializeObject<ApplicationUser>(userJson);
            }

            return user;
        }
    }
}
