using Newtonsoft.Json;

namespace SimPaulOnbase.Core.Boundaries.Auth
{
    public class LoginInput
    {
        [JsonProperty("login")]
        public string Login { get; set; }
        [JsonProperty("password")]
        public string Password { get; set; }
    }
}
