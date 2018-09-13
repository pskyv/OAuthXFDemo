using System;
using System.Collections.Generic;
using System.Text;

namespace OAuthXFDemo.Utils
{
    public static class Constants
    {
        public const string BaseEndpoint = "https://medicalinfo3-login-canary.azurewebsites.net";
        public const string clientId = "xamarin";
        public const string clientSecret = "e61be018-ddf3-40b1-9df2-82db749a2f65";
        public const string scope = "openid role profile email offline_access users_mgmt permissions_api insurance_management_api associate_management_api front_office_api entity_management_api";             
        public const string redirectUri = "com.inflolyseis.xamarin.oauth:/oauth2redirect";

        public const string AccessTokenKey = "OAuthDemo";
    }
}
