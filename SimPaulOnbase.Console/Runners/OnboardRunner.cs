﻿using SimPaulOnbase.Core.Boundaries.Customers;
using SimPaulOnbase.Core.UseCases.Customers;
using SimPaulOnbase.Infraestructure.ApiDataAccess;
using SimPaulOnbase.Infraestructure.Gateways;
using System.Linq;

namespace SimPaulOnbase.Console
{


    public class OnboardRunner
    {
        public void OnWorkflowScriptExecute(Hyland.Unity.Application app, Hyland.Unity.WorkflowEventArgs args)
        {
            OnbaseSettings onbaseSettings = GetOnbaseSettings();

            var statusFilter = new string[]
            {
                "INREVISION"
            };

            try
            {
                var customerRepository = new SimPaulOnbase.Infraestructure.ApiDataAccess.CustomerApiRepository(this.GetApiSettings());
                var onbaseConector = new OnbaseInMemoryConector(app);
                var onbaseCustomerService = new CustomerOnboardOnbaseService(onbaseSettings, onbaseConector, new Logger(), customerRepository);
                var customerIntegrationUseCase = new CustomerOnboardIntegrationUseCase(customerRepository, onbaseCustomerService, new Logger());
                customerIntegrationUseCase.Handle(new CustomerIntegrationInput
                {
                    StatusFilter = statusFilter
                });
            }
            catch (System.Exception ex)
            {
                //System.Diagnostics.Debug.Write(ex.ToString());
                throw ex;
            }

        }

        private OnbaseSettings GetOnbaseSettings()
        {
            return new OnbaseSettings
            {
                CustomerDocumentType = "BKO - Cadastro",
                CustomerDocumentFileType = "Unity Form",
                FormIntegrationID = 117,
                DataSource = "OnBasePROD"
            };
        }

        private CustomerApiSettings GetApiSettings()
        {
            CustomerApiSettings apiSettings = new CustomerApiSettings
            {
                BaseUrl = "https://dev-gapi.simpaul.com.br/",
                IncompletedResource = "dev/backoffice/client/incomplete",
                ApproveResource = "dev/backoffice/client/sinacor",
                ReproveResource = "dev/backoffice/client/{id}/status",
                CustomerResource = "dev/backoffice/client/onboarding",
                LoginResource = "dev/backoffice/authentication",
                UserLogin = "07915143743",
                PasswordLogin = "kZzoYoF+6sxNy/TaVFq603QclLvKlf5/13zhpj3kKEo="
            };

            return apiSettings;
        }

        public void ExecuteIntegrations()
        {           

            var customerRepository = new SimPaulOnbase.Infraestructure.ApiDataAccess.CustomerApiRepository(this.GetApiSettings());

            var customers = customerRepository.GetIncompleted()
            .GetAwaiter()
            .GetResult();

            var customersData = customerRepository
                .GetCustomer()
                .GetAwaiter()
                .GetResult();


            var customer = customersData
                .FirstOrDefault(cm => cm.SinacorAccounts == null || cm.SinacorAccounts.Length == 0);

            var aproveInput = new CustomerApproveInput
            {
                Id = customer.Id,
                CPF = customer.Cpf
            };

            customerRepository.ApproveRegistration(aproveInput)
                .GetAwaiter()
                .GetResult();

            var reproveInput = new CustomerReproveInput
            {
                Id = customer.Id,
                Status = "REPROVADO"
            };

            customerRepository.ReproveRegistration(reproveInput)
                .GetAwaiter()
                .GetResult();
        }


        public void Approve(string customerCPF)
        {
            var customerRepository = new SimPaulOnbase.Infraestructure.ApiDataAccess.CustomerApiRepository(this.GetApiSettings());

            var output = customerRepository
                .GetCustomer(customerCPF)
                .GetAwaiter()
                .GetResult();

            var customer = output.FirstOrDefault();

            var aproveInput = new CustomerApproveInput
            {
                Id = customer.Id,
                CPF = customer.Cpf
            };

            customerRepository.ApproveRegistration(aproveInput)
                .GetAwaiter()
                .GetResult();
        }

        public void Reprove(string customerDocument)
        {

            var customerRepository = new SimPaulOnbase.Infraestructure.ApiDataAccess.CustomerApiRepository(this.GetApiSettings());

            var output = customerRepository
                .GetCustomer(customerDocument)
                .GetAwaiter()
                .GetResult();

            var customer = output.FirstOrDefault();

            var reproveInput = new CustomerReproveInput
            {
                Id = customer.Id,
                Status = "BACKOFFICEDENIED"
            };

            customerRepository.ReproveRegistration(reproveInput)
                .GetAwaiter()
                .GetResult();
        }

        public void GetCustomerOnboard(Hyland.Unity.Application con, string customerDocument)
        {
            OnbaseSettings onbaseSettings = this.GetOnbaseSettings();

            var statusFilter = new string[]
            {
                "INREVISION"
            };

            try
            {
                var customerRepository = new SimPaulOnbase.Infraestructure.ApiDataAccess.CustomerApiRepository(this.GetApiSettings());
                var onbaseConector = new OnbaseInMemoryConector(con);
                var onbaseCustomerService = new CustomerOnboardOnbaseService(onbaseSettings, onbaseConector, new Logger(), customerRepository);
                var customerIntegrationUseCase = new CustomerOnboardIntegrationUseCase(customerRepository, onbaseCustomerService, new Logger());
                customerIntegrationUseCase.Handle(new CustomerIntegrationInput
                {
                    StatusFilter = statusFilter
                });
            }
            catch (System.Exception ex)
            {
                System.Diagnostics.Debug.Write(ex.ToString());
            }

        }
    }
}