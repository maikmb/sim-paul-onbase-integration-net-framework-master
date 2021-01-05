
//using Microsoft.Extensions.Logging;
using SimPaulOnbase.Core.Boundaries.Customers;
using SimPaulOnbase.Core.Repositories;
using SimPaulOnbase.Core.Gateways;
using SimPaulOnbase.Core.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using SimPaulOnbase.Core.Domain;

namespace SimPaulOnbase.Core.UseCases.CustomerRegistration
{
    /// <summary>
    /// CustomerRegistrationUseCase class
    /// </summary>
    public class CustomerRegistrationUseCase : ICustomerRegistrationUseCase
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly ICustomerReRegistrationOnbaseService _customerOnbaseService;
        private readonly ILogger _logger;

        /// <summary>
        /// Constructor for CustomerRegistrationUseCase
        /// </summary>
        /// <param name="customerRepository"></param>
        /// <param name="customerOnbaseService"></param>
        public CustomerRegistrationUseCase(
            ICustomerRepository customerRepository,
            ICustomerReRegistrationOnbaseService customerOnbaseService,
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
        public CustomerIntegrationOutput Handle()
        {

            try
            {
                var customers = _customerRepository
                    .GetRegisterAgain()
                    .GetAwaiter()
                    .GetResult();              

                this._logger.Info("Total Customers count for register again : " + customers.Count);

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
    }
}
