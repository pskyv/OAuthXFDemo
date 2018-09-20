using Refit;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace OAuthXFDemo.Services
{
    [Headers("Content-Type: application/json")]
    public interface IAuthenticationApi
    {
        [Get("/connect/userinfo")]
        Task<HttpResponseMessage> GetUserInfo(CancellationToken cancellationToken);
    }
}
