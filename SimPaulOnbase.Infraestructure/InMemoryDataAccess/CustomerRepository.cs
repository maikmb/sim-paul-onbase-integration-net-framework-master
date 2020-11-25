using Newtonsoft.Json;
using SimPaulOnbase.Core;
using SimPaulOnbase.Core.Repositories;
using SimPaulOnbase.Core.Domain;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using SimPaulOnbase.Core.Boundaries.Customers;

namespace SimPaulOnbase.Infraestructure.InMemoryDataAccess
{
    /// <summary>
    /// CustomerRepository class
    /// </summary>
    public class CustomerRepository : ICustomerRepository
    {
        

        /// <summary>
        /// CustomerRepository constructor
        /// </summary>
        /// <param name="client"></param>
        /// <param name="customerApiSettings"></param>
        public CustomerRepository()
        {

        }

        /// <summary>
        /// Get diverged registrations from SimPaul Customer API
        /// </summary>
        /// <returns></returns>

        public Task<List<CustomerTransactional>> GetIncompleted()
        {
            throw new NotImplementedException();
        }

        public Task ApproveRegistration(CustomerApproveInput input)
        {
            throw new NotImplementedException();
        }

        public Task ReproveRegistration(CustomerReproveInput input)
        {
            throw new NotImplementedException();
        }

        public Task<List<CustomerOnboard>> GetCustomer(string document)
        {
            throw new NotImplementedException();
        }

        public Task<Suitability> GetCustomerSuitability(CustomerOnboard customer)
        {
            throw new NotImplementedException();
        }

        public Task<List<CustomerTransactional>> GetRegisterAgain()
        {
            throw new NotImplementedException();
        }
    }
}
