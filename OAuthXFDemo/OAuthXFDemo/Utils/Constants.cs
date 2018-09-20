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

        public const string SyncfusionLicenseKey = "MjI0MzBAMzEzNjJlMzIyZTMwTzdkSW5TOXhCeVR6WUVqOStUZ1c2c3FrK2RzZDJ4V01ENTN4dGpkK0dWdz0=;" +
                                                   "MjI0MzFAMzEzNjJlMzIyZTMwQzE4bGx6Wmk0ejdzYlRZUEN5bUljRVltS2lLRU40OHhSMnZIRWFZLzR4bz0=;" +
                                                   "MjI0MzJAMzEzNjJlMzIyZTMwZFMzdWF2ZVpHd3Bhak1JdWtweGJxSHBlZmJwSi8zQjhudkRxcjZVcEFBMD0=;" +
                                                   "MjI0MzNAMzEzNjJlMzIyZTMwbktkMVlPaWVDTGtkREFuVWxjT01lMGJaZ1JjRHp1S0gyS2ZVWCtjTnBZTT0=;" +
                                                   "MjI0MzRAMzEzNjJlMzIyZTMwS1Qrc2ZLWlhlM2prTmdTZmcrVXBFOFJzSDRqWE9vNCs3ZVRnSll4UGFpMD0=;" +
                                                   "MjI0MzVAMzEzNjJlMzIyZTMwQUxkbDZIM2szR1hnY01kQ1JKWGNHRGx0RW9VNjY2dVNHM0h5ZUNHRVQ1ST0=;" +
                                                   "MjI0MzZAMzEzNjJlMzIyZTMwRzh0YUNvZXJuRERaeEhNbHpFQm1Bcy94Y0NXOE83T0lyZ1dlL211Ny80ND0=;" +
                                                   "MjI0MzdAMzEzNjJlMzIyZTMwa29SckJqdm1Ybll2ZWNVN0lkLytJcEl4SGpDbnJpYU9ycTlpME5NY0VzND0=;" +
                                                   "MjI0MzhAMzEzNjJlMzIyZTMwZDJQODFSYzdnTEZQRU42ZGhNMk1LZFY0amVrR0ttZnJKOGlQMU51ckhEND0=";
    }
}
