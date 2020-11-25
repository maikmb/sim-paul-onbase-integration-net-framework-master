using SimPaulOnbase.Core.Boundaries.Customers;
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
                var customerRepository = new CustomerApiRepository(this.GetApiSettings());
                var onbaseConector = new OnbaseInMemoryConector(app);
                var onbaseCustomerService = new CustomerOnboardOnbaseService(onbaseSettings, onbaseConector, new FileLogger(), customerRepository);
                var customerIntegrationUseCase = new CustomerOnboardIntegrationUseCase(customerRepository, onbaseCustomerService, new FileLogger());
                customerIntegrationUseCase.Handle(new CustomerIntegrationInput
                {
                    StatusFilter = statusFilter
                });
            }
            catch (System.Exception ex)
            {

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
                DataSource = "OnBasestg"
            };
        }

        private CustomerApiSettings GetApiSettings()
        {
            CustomerApiSettings apiSettings = new CustomerApiSettings
            {
                BaseUrl = "https://stg-gapi.simpaul.com.br/",
                IncompletedResource = "stg/backoffice/client/incomplete",
                ApproveResource = "stg/backoffice/client/sinacor",
                ReproveResource = "stg/backoffice/client/{id}/status",
                CustomerResource = "stg/backoffice/client/onboarding",
                LoginResource = "stg/backoffice/authentication",
                SuitabilityResource = "stg/backoffice/client/{id}/suitability",
                RegisterAginResource = "stg/backoffice/client/reregister",
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
                var onbaseCustomerService = new CustomerOnboardOnbaseService(onbaseSettings, onbaseConector, new FileLogger(), customerRepository);
                var customerIntegrationUseCase = new CustomerOnboardIntegrationUseCase(customerRepository, onbaseCustomerService, new FileLogger());
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
