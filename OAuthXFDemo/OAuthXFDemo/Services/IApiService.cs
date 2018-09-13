using OAuthXFDemo.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace OAuthXFDemo.Services
{
    public interface IApiService
    {
        Task<ApplicationUser> GetUserInfo();
    }
}
