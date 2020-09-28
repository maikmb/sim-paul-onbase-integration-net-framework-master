using SimPaulOnbase.Core.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SimPaulOnbase.Core.Gateways
{
    public interface ICustomerOnboardOnbaseService
    {
        Task Handle(List<CustomerOnboard> customer);
    }
}
