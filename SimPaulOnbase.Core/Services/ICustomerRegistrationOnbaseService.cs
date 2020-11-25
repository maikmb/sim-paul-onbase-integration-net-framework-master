using SimPaulOnbase.Core.Domain;
using System.Collections.Generic;

namespace SimPaulOnbase.Core.Gateways
{
    public interface ICustomerRegistrationOnbaseService
    {
        void Handle(List<CustomerTransactional> customer);
    }
}
