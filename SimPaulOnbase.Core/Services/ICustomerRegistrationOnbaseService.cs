using SimPaulOnbase.Core.Domain;
using System.Collections.Generic;

namespace SimPaulOnbase.Core.Gateways
{
    public interface ICustomerReRegistrationOnbaseService
    {
        void Handle(List<CustomerReRegistration> customer);
    }
}
