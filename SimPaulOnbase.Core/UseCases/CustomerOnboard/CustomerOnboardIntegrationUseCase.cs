
//using Microsoft.Extensions.Logging;
using SimPaulOnbase.Core.Boundaries.Customers;
using SimPaulOnbase.Core.Repositories;
using SimPaulOnbase.Core.Gateways;
using SimPaulOnbase.Core.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using SimPaulOnbase.Core.Domain;

namespace SimPaulOnbase.Core.UseCases.Customers
{
    /// <summary>
    /// CustomerIntegrationUseCase class
    /// </summary>
    public class CustomerOnboardIntegrationUseCase : ICustomerOnboardIntegrationUseCase
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly ICustomerOnboardOnbaseService _customerOnbaseService;
        private readonly ILogger _logger;

        /// <summary>
        /// Constructor for CustomerIntegrationUseCase
        /// </summary>
        /// <param name="customerRepository"></param>
        /// <param name="customerOnbaseService"></param>
        public CustomerOnboardIntegrationUseCase(
            ICustomerRepository customerRepository,
            ICustomerOnboardOnbaseService customerOnbaseService,
            ILogger logger
            )
        {
            _customerRepository = customerRepository;
            _customerOnbaseService = customerOnbaseService;
            _logger = logger;
        }


        /// <summary>
        /// Method Handle
        /// </summary>
        /// <param name="customerIntegrationInput"></param>
        /// <returns>CustomerIntegrationOutput</returns>
        public CustomerIntegrationOutput Handle(CustomerIntegrationInput customerIntegrationInput)
        {

            try
            {
                var customers = _customerRepository
                    .GetCustomer()
                    .GetAwaiter()
                    .GetResult();

                if (customerIntegrationInput.StatusFilter != null && customerIntegrationInput.StatusFilter.Length > 0)
                {
                    customers = FilterCustomers(customerIntegrationInput, customers);                    
                }

                this._logger.Info("Total Registrations Count: " + customers.Count);

                _customerOnbaseService.Handle(customers);
                return new CustomerIntegrationOutput(customers.Count);

            }
            catch (CustomerApiRequestException ex)
            {
                this._logger.Error("Error on retrieve data from api. Check exception for details.", ex);
            }
            catch (OnbaseConnectionException ex)
            {
                this._logger.Error("Cound't connect to onbase server. Check exception for details.", ex);
            }

            return new CustomerIntegrationOutput(0);
        }

        private static List<CustomerOnboard> FilterCustomers(CustomerIntegrationInput customerIntegrationInput, List<CustomerOnboard> customers)
        {
            var filteredCustomers = new List<CustomerOnboard>();
            foreach (var customer in customers)
            {
                if (customer.ClientStatus != null && customerIntegrationInput.StatusFilter.Contains(customer.ClientStatus.Code))
                {
                    filteredCustomers.Add(customer);
                }
            }

            return filteredCustomers;
        }
    }
}
