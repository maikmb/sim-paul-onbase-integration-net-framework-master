using SimPaulOnbase.Console.Settings;
using SimPaulOnbase.Core.Boundaries.Customers;
using SimPaulOnbase.Core.UseCases.CustomerRegistration;
using SimPaulOnbase.Infraestructure.ApiDataAccess;
using SimPaulOnbase.Infraestructure.Gateways;

namespace SimPaulOnbase.Console
{

    public class RegistrationRunner
    {
        public void OnWorkflowScriptExecute(Hyland.Unity.Application app)
        {
            var onbaseSettings = SettingsService.GetOnbaseSettings();
            var logger = new Logger();            

            try
            {
                var customerRepository = new CustomerApiRepository(SettingsService.GetApiSettings());
                var onbaseConector = new OnbaseInMemoryConector(app);
                var onbaseCustomerService = new CustomerRegistrationOnbaseService(onbaseSettings, onbaseConector, logger);

                var customerIntegrationUseCase = new CustomerRegistrationUseCase(customerRepository, onbaseCustomerService, logger);
                customerIntegrationUseCase.Handle();
            }
            catch (System.Exception ex)
            {
                logger.Error(ex.Message);
                throw ex;
            }

        }

        public void Approve(string customerCPF)
        {
            var customerRepository = new CustomerApiRepository(SettingsService.GetApiSettings());            

            var aproveInput = new CustomerRegistrationInput
            {
                CPF = customerCPF
            };

            customerRepository.ApproveRegistrationAgain(aproveInput)
                .GetAwaiter()
                .GetResult();
        }
    }
}
