using CarService.Models.Dto;

namespace CarService.DL.Interfaces
{
    public interface ICustomerRepository
    {
        Task AddCustomer(Customer customer);
        Task DeleteCustomer(Guid id);
        Task<List<Customer>> GetAllCustomers();
        Task<Customer?> GetById(Guid id);
    }
}