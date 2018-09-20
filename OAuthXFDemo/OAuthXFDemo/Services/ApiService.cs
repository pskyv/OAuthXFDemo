using Newtonsoft.Json;
using OAuthXFDemo.Models;
using OAuthXFDemo.Utils;
using Polly;
using Refit;
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
                _client.BaseAddress = new Uri(Constants.BaseEndpoint);
                //_client.DefaultRequestHeaders.Add("Authorization", "Bearer " + accessToken);
            }
        }

        public async Task<ApplicationUser> GetUserInfoAsync()
        {
            var cts = new CancellationTokenSource();
            var task = ExecuteRequestAsync(RestService.For<IAuthenticationApi>(_client).GetUserInfo(cts.Token));
            runningTasks.Add(task.Id, cts);

            var response = await task;

            ApplicationUser user = null;
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var userJson = await response.Content.ReadAsStringAsync();
                user = JsonConvert.DeserializeObject<ApplicationUser>(userJson);
            }

            return user;
        }

        public async Task<HttpResponseMessage> ExecuteRequestAsync(Task<HttpResponseMessage> request)
        {
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
                    var result = await request;
                    
                    if(result.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                    {
                        //Access token expired
                    }
                    runningTasks.Remove(request.Id);
                    return result;
                });

            return response;
        }
    }
}
