using SimPaulOnbase.Core.Domain;

using System.Collections.Generic;
namespace SimPaulOnbase.Core.Gateways
{
    public interface ICustomerTransactionalOnbaseService
    {
        void Handle(List<CustomerTransactional> customer);
    }
}
