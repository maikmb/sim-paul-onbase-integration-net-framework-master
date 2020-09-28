//using Microsoft.Extensions.Logging;
using SimPaulOnbase.Core.Boundaries.Customers;
using SimPaulOnbase.Core.Repositories;
using SimPaulOnbase.Core.Gateways;
using SimPaulOnbase.Core.Exceptions;
using System;
using System.Linq;

namespace SimPaulOnbase.Core.UseCases.Customers
{
    /// <summary>
    /// CustomerIntegrationUseCase class
    /// </summary>
    public class CustomerIncompledIntegrationUseCase : ICustomerIncompledIntegrationUseCase
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly ICustomerTransactionalOnbaseService _customerOnbaseService;
        private readonly ILogger _logger;

        /// <summary>
        /// Constructor for CustomerIntegrationUseCase
        /// </summary>
        /// <param name="customerRepository"></param>
        /// <param name="customerOnbaseService"></param>
        public CustomerIncompledIntegrationUseCase(
            ICustomerRepository customerRepository,
            ICustomerTransactionalOnbaseService customerOnbaseService,
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
        /// <returns>CustomerIntegrationOutput</returns>
        public CustomerIntegrationOutput Handle()
        {

            try
            {
                var divergedRegistrations = _customerRepository
                    .GetIncompleted()
                    .GetAwaiter()
                    .GetResult();

                this._logger.Info("Stoped Registrations Count: " + divergedRegistrations.Count);
                _customerOnbaseService.Handle(divergedRegistrations.ToList());

                return new CustomerIntegrationOutput(divergedRegistrations.Count);

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
