using Newtonsoft.Json;

namespace SimPaulOnbase.Core.Boundaries.Customers
{
    public class CustomerApproveInput
    {
        /// <summary>
        /// Id
        /// </summary>
        [JsonProperty("idClient")]
        public long Id { get; set; }

        /// <summary>
        /// CPF
        /// </summary>
        [JsonProperty("cpf")]
        public string CPF { get; set; }
    }
}
