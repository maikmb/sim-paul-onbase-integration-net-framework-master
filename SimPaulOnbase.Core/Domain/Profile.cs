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
        public DateTime? Created { get; set; }
        public SuitabilityProfile SuitabilityProfile { get; set; }
    }
}
