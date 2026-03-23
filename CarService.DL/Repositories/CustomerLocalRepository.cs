using CarService.Models.Dto;
using CarService.DL.Interfaces;
using CarService.DL.LocalDb;

namespace CarService.DL.Repositories
{
    public class CustomerLocalRepository : ICustomerRepository
    {
        public Task AddCustomer(Customer customer)
        {
            StaticDb.Customers.Add(customer);
            return Task.CompletedTask;
        }

        public Task DeleteCustomer(Guid id)
        {
            StaticDb.Customers.RemoveAll(c => c.Id == id);
            return Task.CompletedTask;
        }

        public Task<List<Customer>> GetAllCustomers()
        {
            return Task.FromResult(StaticDb.Customers);
        }

        public Task<Customer?> GetById(Guid id)
        {
            return Task.FromResult(StaticDb.Customers.FirstOrDefault(c => c.Id == id));
        }
    }
}