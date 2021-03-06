﻿using Hyland.Unity;
using Hyland.Unity.UnityForm;
using SimPaulOnbase.Core.Gateways;
using SimPaulOnbase.Core.Domain;
using System.Collections.Generic;
using SimPaulOnbase.Infraestructure.Gateways.Forms;

namespace SimPaulOnbase.Infraestructure.Gateways
{
    /// <summary>
    /// CustomerOnbaseIntegration class
    /// </summary>
    public class CustomerTransactionalOnbaseService : OnbaseServiceBase, ICustomerTransactionalOnbaseService
    {
        private readonly OnbaseSettings _onbaseSettings;
        private readonly ILogger _logger;

        public CustomerTransactionalOnbaseService(OnbaseSettings onbaseSettings, IOnbaseConector _onbaseConector, ILogger logger) : base(onbaseSettings, _onbaseConector)
        {
            this._onbaseSettings = onbaseSettings;
            this._logger = logger;
        }

        public void Handle(List<CustomerTransactional> divergedRegistrations)
        {
            this.GetConector();

            foreach (var customer in divergedRegistrations)
            {
                IntegrateCustomer(customer);

                this._logger.Info("Integrated customer: " + customer.Name);
            }
        }

        private void IntegrateCustomer(CustomerTransactional customer)
        {
            FormTemplate formTemplate = this.FindFormTemplate(_onbaseSettings.FormIntegrationID);
            StoreNewUnityFormProperties onbaseStore = this.InitNewForm(formTemplate);
            MapCustomerFieldsToOnbase(customer, onbaseStore, formTemplate);
            this.StoreNewUnityForm(onbaseStore);
        }

        private void MapCustomerFieldsToOnbase(CustomerTransactional customer, StoreNewUnityFormProperties onbaseStore, FormTemplate formTemplate)
        {
        


            var customerForm = new CustomerTransactionalForm(onbaseStore, formTemplate);
            customerForm.ApplyBasicData(customer);
            customerForm.ApplyAddress(customer.Addresses);
            customerForm.ApplyAccounts(customer.Accounts);
            customerForm.ApplyWork(customer.Work);
            customerForm.ApplyDocument(customer.Document);
            customerForm.SuitabilityData(customer.Suitability);
        }
    }
}
