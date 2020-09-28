using MongoDB.Driver;
using SimPaulOnbase.Core.Repositories;
using SimPaulOnbase.Core.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;
using SimPaulOnbase.Core.Boundaries.Customers;

namespace SimPaulOnbase.Infraestructure.MongoDbDataAccess
{
    /// <summary>
    /// CustomerRepository class
    /// </summary>
    public class CustomerMongoDbRepository : ICustomerRepository
    {

        /// <summary>
        /// CustomerRepository constructor
        /// </summary>
        /// <param name="client"></param>
        /// <param name="customerApiSettings"></param>
        public CustomerMongoDbRepository(string conectionString, string dataBase)
        {
            var cliente = new MongoClient(conectionString);
            this.DataBase = cliente.GetDatabase(dataBase);
        }

        public IMongoDatabase DataBase { get; }

        /// <summary>
        /// Get diverged registrations from SimPaul Customer API
        /// </summary>
        /// <returns></returns>

        public async Task<List<CustomerTransactional>> GetIncompleted()
        {
            var collection = this.DataBase.GetCollection<CustomerTransactional>("simpaul.onboarding");
            var cursor = await collection.FindAsync(_ => true);
            return cursor.ToList();
        }

        public Task ApproveRegistration(CustomerApproveInput input)
        {
            throw new System.NotImplementedException();
        }

        public Task ReproveRegistration(CustomerReproveInput input)
        {
            throw new System.NotImplementedException();
        }

        public Task<List<CustomerOnboard>> GetCustomer(string document)
        {
            throw new System.NotImplementedException();
        }

        public Task<Suitability> GetCustomerSuitability(CustomerOnboard customer)
        {
            throw new System.NotImplementedException();
        }
    }
}
