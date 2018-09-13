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

namespace OAuthXFDemo.Services
{    
    public class ApiService : IApiService
    {
        private string userInfoUrl = $"{Constants.BaseEndpoint}/connect/userinfo";
        private HttpClient _client;

        public ApiService()
        {
            ConfigureHttpClient();            
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
