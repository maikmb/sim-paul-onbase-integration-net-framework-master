using System.Linq;
using SimPaulOnbase.Core.Boundaries.Customers;
using SimPaulOnbase.Infraestructure.ApiDataAccess;
using SimPaulOnbase.Core.UseCases.Customers;
using SimPaulOnbase.Infraestructure.Gateways;

namespace SimPaulOnbase.Console
{
   

    public class IncompletedRunner
    {
        public void OnWorkflowScriptExecute(Hyland.Unity.Application app, Hyland.Unity.WorkflowEventArgs args)
        {

            OnbaseSettings onbaseSettings = new OnbaseSettings
            {
                CustomerDocumentType = "BKO - Cadastro",
                CustomerDocumentFileType ="Unity Form",
                FormIntegrationID = 117
            };

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

            try
            {
                var customerRepository = new SimPaulOnbase.Infraestructure.ApiDataAccess.CustomerApiRepository(apiSettings);

                var onbaseConector = new OnbaseInMemoryConector(app);
                var onbaseCustomerService = new CustomerTransactionalOnbaseService(onbaseSettings, onbaseConector, new Logger());
                var customerIntegrationUseCase = new CustomerIncompledIntegrationUseCase(customerRepository, onbaseCustomerService, new Logger());
                customerIntegrationUseCase.Handle();
            }
            catch (System.Exception ex)
            {
                System.Diagnostics.Debug.Write(ex.ToString());
            }

        }

        public void ExecuteIntegrations()
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


            var customerRepository = new SimPaulOnbase.Infraestructure.ApiDataAccess.CustomerApiRepository(apiSettings);

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


        public void Approve()
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


            var customerRepository = new SimPaulOnbase.Infraestructure.ApiDataAccess.CustomerApiRepository(apiSettings);

            var output = customerRepository
                .GetCustomer("44433322288")
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

        public void Reprove()
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


            var customerRepository = new SimPaulOnbase.Infraestructure.ApiDataAccess.CustomerApiRepository(apiSettings);

            var output = customerRepository
                .GetCustomer("44433322288")
                .GetAwaiter()
                .GetResult();

            var customer = output.FirstOrDefault();

            var reproveInput = new CustomerReproveInput
            {
                Id = customer.Id,
                Status = "REPROVADO"
            };

            customerRepository.ReproveRegistration(reproveInput)
                .GetAwaiter()
                .GetResult();
        }
    }
}
