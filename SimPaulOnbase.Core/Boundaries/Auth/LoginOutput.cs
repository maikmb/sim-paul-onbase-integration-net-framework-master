using Newtonsoft.Json;

namespace SimPaulOnbase.Core.Boundaries.Auth
{
    public class LoginOutput
    {
        [JsonProperty("token")]
        public string Token { get; set; }
    }
}
