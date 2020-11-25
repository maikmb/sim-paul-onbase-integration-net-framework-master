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
            var logger = new FileLogger("C:\\Temp\\OnbaseIntegration.log");            

            try
            {

                logger.Info("Iniciando processo de integração Recadastro");

                var customerRepository = new CustomerApiRepository(SettingsService.GetApiSettings());
                var onbaseConector = new OnbaseInMemoryConector(app);
                var onbaseCustomerService = new CustomerRegistrationOnbaseService(onbaseSettings, onbaseConector, logger);

                var customerIntegrationUseCase = new CustomerRegistrationUseCase(customerRepository, onbaseCustomerService, logger);
                customerIntegrationUseCase.Handle();

                logger.Info("Importação de Recadastrado executada com sucesso");
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
