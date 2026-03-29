using CarService.DL.Interfaces;
using CarService.DL.LocalDb;
using CarService.Models.Dto;
using Microsoft.Extensions.Logging;

namespace CarService.DL.Repositories
{
    internal class CustomerStaticRepository : ICustomerRepository
    {
        private readonly ILogger<CustomerStaticRepository> _logger;

        public CustomerStaticRepository(ILogger<CustomerStaticRepository> logger)
        {
            _logger = logger;
        }

        public Task AddAsync(Customer? customer)
        {
            if (customer != null)
            {
                StaticDb.Customers.Add(customer);
            }

            return Task.CompletedTask;
        }

        public Task<List<Customer>> GetAllAsync()
        {
            try
            {
                return Task.FromResult(StaticDb.Customers);
            }
            catch (Exception e)
            {
                _logger.LogError($"Error in {nameof(GetAllAsync)}:{e.Message}-{e.StackTrace}");
            }

            return Task.FromResult(new List<Customer>());
        }

        public Task<Customer?> GetByIdAsync(Guid id)
        {
            if (id == Guid.Empty) return Task.FromResult<Customer?>(null);

            var customer = StaticDb.Customers.FirstOrDefault(c => c.Id == id);
            return Task.FromResult(customer);
        }

        public async Task DeleteAsync(Guid id)
        {
            if (id == Guid.Empty) return;

            var customer = await GetByIdAsync(id);

            if (customer != null)
            {
                StaticDb.Customers.Remove(customer);
            }
        }

        public Task<List<Customer>> GetAll()
        {
            throw new NotImplementedException();
        }
    }
}