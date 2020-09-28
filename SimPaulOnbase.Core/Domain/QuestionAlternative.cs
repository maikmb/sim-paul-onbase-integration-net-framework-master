using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimPaulOnbase.Core.Domain
{
    public class QuestionAlternative
    {
        public long Id { get; set; }
        public DateTimeOffset Created { get; set; }
        public long Score { get; set; }
        public object Template { get; set; }
        public string Type { get; set; }
        public object Status { get; set; }
        public long OrderQuestion { get; set; }
        public long OrderAlternative { get; set; }
        public SuitabilityQuestion SuitabilityQuestion { get; set; }
        public SuitabilityAlternative SuitabilityAlternative { get; set; }
    }
}
