using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimPaulOnbase.Core.Domain
{
    public class Profile
    {
        public string Id { get; set; }
        public string TotalScore { get; set; }
        public string Created { get; set; }        

        [JsonProperty("dtExpiration")]
        public string DtExpiration{ get; set; }

        public SuitabilityProfile SuitabilityProfile { get; set; }
    }
}
