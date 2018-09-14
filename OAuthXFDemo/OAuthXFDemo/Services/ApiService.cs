using Newtonsoft.Json;
using OAuthXFDemo.Models;
using OAuthXFDemo.Utils;
using Polly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
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
        Dictionary<int, CancellationTokenSource> runningTasks = new Dictionary<int, CancellationTokenSource>();


        public ApiService()
        {
            ConfigureHttpClient();

            _networkAccess = Connectivity.NetworkAccess;
            Connectivity.ConnectivityChanged += OnConnectivityChanged;
        }

        private void OnConnectivityChanged(object sender, ConnectivityChangedEventArgs e)
        {
            _networkAccess = e.NetworkAccess; 
            if(_networkAccess == NetworkAccess.None)
            {
                CancelRunningTasks();
            }
        }

        private void CancelRunningTasks()
        {
            var items = runningTasks.ToList();
            foreach (var item in items)
            {
                item.Value.Cancel();
                runningTasks.Remove(item.Key);
            }
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
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, userInfoUrl);
            var task = ExecuteRequestAsync(request);            

            ApplicationUser user = null;
            if (task.Result.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var userJson = await task.Result.Content.ReadAsStringAsync();
                user = JsonConvert.DeserializeObject<ApplicationUser>(userJson);
            }

            return user;
        }

        public async Task<HttpResponseMessage> ExecuteRequestAsync(HttpRequestMessage request)
        {
            var cts = new CancellationTokenSource();
            HttpResponseMessage response = new HttpResponseMessage();

            //check connectivity first
            if(_networkAccess.Equals(NetworkAccess.None))
            {
                var responseMsg = "There's no network connection";
                response.StatusCode = System.Net.HttpStatusCode.BadRequest;
                response.Content = new StringContent(responseMsg);
                HelperFunctions.ShowToastMessage(ToastMessageType.Error, responseMsg);
                return response;
            }

            response = await Policy
                .Handle<HttpRequestException>()
                .WaitAndRetryAsync
                (
                    retryCount: 1,
                    sleepDurationProvider: retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt))
                )
                .ExecuteAsync(async () =>
                {
                    Task<HttpResponseMessage> task = _client.SendAsync(request);
                    runningTasks.Add(task.Id, cts);
                    var result = task.Result;
                    
                    if(result.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                    {
                        //Access token expired
                    }
                    runningTasks.Remove(task.Id);
                    return result;
                });

            return response;
        }
    }
}
