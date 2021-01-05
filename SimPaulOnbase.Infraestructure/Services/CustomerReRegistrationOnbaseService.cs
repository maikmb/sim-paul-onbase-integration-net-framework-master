using Hyland.Unity;
using Hyland.Unity.UnityForm;
using SimPaulOnbase.Core.Gateways;
using SimPaulOnbase.Core.Domain;
using System.Collections.Generic;
using SimPaulOnbase.Infraestructure.Gateways.Forms;
using SimPaulOnbase.Core.Repositories;

namespace SimPaulOnbase.Infraestructure.Gateways
{
    /// <summary>
    /// CustomerOnbaseIntegration class
    /// </summary>
    public class CustomerReRegistrationOnbaseService : OnbaseServiceBase, ICustomerReRegistrationOnbaseService
    {
        private readonly OnbaseSettings _onbaseSettings;
        private readonly ILogger _logger;
        private readonly ICustomerRepository _customerRepository;

        public CustomerReRegistrationOnbaseService(OnbaseSettings onbaseSettings, IOnbaseConector _onbaseConector, ICustomerRepository _customerRepository, ILogger logger) : base(onbaseSettings, _onbaseConector)
        {
            this._onbaseSettings = onbaseSettings;
            this._logger = logger;
            this._customerRepository = _customerRepository;
        }

        public void Handle(List<CustomerReRegistration> divergedRegistrations)
        {
            this.GetConector();

            foreach (var customer in divergedRegistrations)
            {
                IntegrateCustomer(customer);
                this._logger.Info("Integrated customer: " + customer.Name);
            }
        }

        private Document IntegrateCustomer(CustomerReRegistration customer)
        {
            FormTemplate formTemplate = this.FindFormTemplate(_onbaseSettings.FormIntegrationID);
            StoreNewUnityFormProperties onbaseStore = this.InitNewForm(formTemplate);
            MapCustomerFieldsToOnbase(customer, onbaseStore, formTemplate);
            var output = this.StoreNewUnityForm(onbaseStore);
            return output;
        }

        private void MapCustomerFieldsToOnbase(CustomerReRegistration customer, StoreNewUnityFormProperties onbaseStore, FormTemplate formTemplate)
        {           

            var customerForm = new CustomerReRegistrationForm(onbaseStore, formTemplate);
            customerForm.ApplyBasicData(customer);
            customerForm.ApplyAddress(customer.Addresses);
            customerForm.ApplyAccounts(customer.Accounts);
            customerForm.ApplyWork(customer.Work);
            customerForm.ApplyDeclarations(customer.Declarations);
            customerForm.ApplyInvestiments(customer.Investments);
            customerForm.ApplyFatca(customer.Fatca);
            customerForm.ApplyDocument(customer.Document);


            if (!string.IsNullOrEmpty(customer.IdClient))
            {
                Suitability suitability = this._customerRepository
                .GetCustomerSuitability(customer.IdClient)
                .GetAwaiter()
                .GetResult();

                customerForm.SuitabilityData(suitability);
            }            
        }        
    }
}
