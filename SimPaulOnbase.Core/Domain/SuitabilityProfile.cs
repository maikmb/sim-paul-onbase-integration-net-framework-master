using System;

namespace SimPaulOnbase.Core.Domain
{
    public class SuitabilityProfile
    {
        public string IdProfile { get; set; }
        public string Profile { get; set; }
        public string Description { get; set; }
        public string Score { get; set; }
        public string Created { get; set; }
        public string DtExpiration { get; set; }
    }
}
