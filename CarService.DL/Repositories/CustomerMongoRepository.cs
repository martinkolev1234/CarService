using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using CarService.DL.Interfaces;
using CarService.Models.Configurations;
using CarService.Models.Dto;

namespace CarService.DL.Repositories
{
    public class CustomerMongoRepository : ICustomerRepository
    {
        private readonly IOptionsMonitor<MongoDbConfiguration> _mongoDbConfiguration;
        private readonly ILogger<CustomerMongoRepository> _logger;
        private readonly IMongoCollection<Customer> _customersCollection;

        public CustomerMongoRepository(
            IOptionsMonitor<MongoDbConfiguration> mongoDbConfiguration,
            ILogger<CustomerMongoRepository> logger)
        {
            _mongoDbConfiguration = mongoDbConfiguration;
            _logger = logger;

            var client = new MongoClient(_mongoDbConfiguration.CurrentValue.ConnectionString);
            var database = client.GetDatabase(_mongoDbConfiguration.CurrentValue.DatabaseName);

            _customersCollection = database.GetCollection<Customer>($"{nameof(Customer)}s");
        }

        public async Task AddCustomer(Customer customer)
        {
            if (customer == null) return;

            try
            {
                await _customersCollection.InsertOneAsync(customer);
            }
            catch (Exception e)
            {
                _logger.LogError($"Error adding Customer to DB: {0} - {1}", e.Message, e.StackTrace);
            }
        }

        public async Task DeleteCustomer(Guid id)
        {
            if (id == Guid.Empty) return;

            try
            {
                var result = await _customersCollection.DeleteOneAsync(c => c.Id == id);

                if (result.DeletedCount == 0)
                {
                    _logger.LogWarning($"No customer found with id: {id} to delete.");
                }
            }
            catch (Exception e)
            {
                _logger.LogError($"Error in method {nameof(DeleteCustomer)}: {e.Message} - {e.StackTrace}");
            }
        }

        public async Task<List<Customer>> GetAllCustomers()
        {
            try
            {
                return await _customersCollection.Find(_ => true).ToListAsync();
            }
            catch (Exception e)
            {
                _logger.LogError($"Error in method {nameof(GetAllCustomers)}: {e.Message} - {e.StackTrace}");
                return new List<Customer>();
            }
        }

        public async Task<Customer?> GetById(Guid id)
        {
            if (id == Guid.Empty) return default;

            try
            {
                return await _customersCollection.Find(c => c.Id == id).FirstOrDefaultAsync();
            }
            catch (Exception e)
            {
                _logger.LogError($"Error in method {nameof(GetById)}: {e.Message} - {e.StackTrace}");
            }

            return default;
        }
    }
}