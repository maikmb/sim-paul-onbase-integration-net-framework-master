using SimPaulOnbase.Core.Boundaries.Customers;

namespace SimPaulOnbase.Core.UseCases.Customers
{
    /// <summary>
    /// CustomerIntegrationUseCase interface
    /// </summary>
    public interface ICustomerIncompledIntegrationUseCase
    {
        CustomerIntegrationOutput Handle();
    }
}
    