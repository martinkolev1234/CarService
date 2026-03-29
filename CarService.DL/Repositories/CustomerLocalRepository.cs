using CarService.DL.LocalDb;
using CarService.Models.Dto;
using CarService3.DL.Interfaces;
using Microsoft.Extensions.Logging;

namespace CarService3.DL.Repositories
{
    internal class CustomerStaticRepository : ICustomerRepository
    {
        private readonly ILogger<CustomerStaticRepository> _logger;

        public CustomerStaticRepository(ILogger<CustomerStaticRepository> logger)
        {
            _logger = logger;
        }

        public Task Add(Customer? customer)
        {
            if (customer != null)
            {
                StaticDb.Customers.Add(customer);
            }

            return Task.CompletedTask;
        }

        public Task<List<Customer>> GetAll()
        {
            try
            {
                return Task.FromResult(StaticDb.Customers);
            }
            catch (Exception e)
            {
                _logger.LogError($"Error in {nameof(GetAll)}:{e.Message}-{e.StackTrace}");
            }

            return Task.FromResult(new List<Customer>());
        }

        public Task<Customer?> GetById(Guid id)
        {
            if (id == Guid.Empty) return Task.FromResult<Customer?>(null);

            var customer = StaticDb.Customers.FirstOrDefault(c => c.Id == id);
            return Task.FromResult(customer);
        }

        // We make this method 'async' so we can 'await' the GetById method
        public async Task Delete(Guid id)
        {
            if (id == Guid.Empty) return;

            var customer = await GetById(id);

            if (customer != null)
            {
                StaticDb.Customers.Remove(customer);
            }
        }
    }
}