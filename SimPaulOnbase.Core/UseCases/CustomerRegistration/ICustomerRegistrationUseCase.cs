using SimPaulOnbase.Core.Boundaries.Customers;

namespace SimPaulOnbase.Core.UseCases.CustomerRegistration
{
    public interface ICustomerRegistrationUseCase
    {
        CustomerIntegrationOutput Handle();
    }
}
