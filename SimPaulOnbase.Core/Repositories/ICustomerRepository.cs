using SimPaulOnbase.Core.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;
using SimPaulOnbase.Core.Boundaries.Customers;

namespace SimPaulOnbase.Core.Repositories
{
    /// <summary>
    /// ICustomerRepository interface
    /// </summary>
    public interface ICustomerRepository
    {

        /// <summary>
        /// Get incompleted registrations
        /// </summary>
        /// <returns></returns>
        Task<List<CustomerTransactional>> GetIncompleted();

        /// <summary>
        /// Get customer for register again
        /// </summary>
        /// <returns></returns>
        Task<List<CustomerReRegistration>> GetRegisterAgain();

        /// <summary>
        /// Approve Registrations
        /// </summary>
        Task ApproveRegistration(CustomerApproveInput input);

        /// <summary>
        /// Reprove Registrations
        /// </summary>
        /// <param name="input"></param>
        Task ReproveRegistration(CustomerReproveInput input);

        /// <summary>
        /// Get Customers OnBoard
        /// </summary>
        Task<List<CustomerOnboard>> GetCustomer(string document = "");

        /// <summary>
        /// Get Customer Suitability Profile
        /// </summary>
        /// <param name="customer"></param>
        /// <returns></returns>
        Task<Suitability> GetCustomerSuitability(string suitabilityID);
    }
}
