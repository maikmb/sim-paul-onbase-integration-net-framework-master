using Newtonsoft.Json;

namespace SimPaulOnbase.Core.Boundaries.Customers
{
    public class CustomerRegistrationInput
    {
        /// <summary>
        /// CPF
        /// </summary>
        [JsonProperty("cpf")]
        public string CPF { get; set; }
    }
}
