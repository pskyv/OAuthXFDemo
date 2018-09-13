using Newtonsoft.Json;

namespace OAuthXFDemo.Models
{
    public class ApplicationUser
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("role")]
        public string Role { get; set; }
    }
}
