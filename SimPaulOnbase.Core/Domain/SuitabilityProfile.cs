using System;

namespace SimPaulOnbase.Core.Domain
{
    public class SuitabilityProfile
    {
        public long? IdProfile { get; set; }
        public string Profile { get; set; }
        public string Description { get; set; }
        public long? Score { get; set; }
        public DateTime? Created { get; set; }
        public DateTime? DtExpiration { get; set; }
    }
}
