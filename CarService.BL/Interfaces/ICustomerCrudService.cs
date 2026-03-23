using CarService.Models.Dto;

namespace CarService.BL.Interfaces
{
    public interface ICustomerCrudService
    {
        Task AddCustomer(Customer customer);
        Task DeleteCustomer(Guid id);
        Task<List<Customer>> GetAllCustomers();
        Task<Customer?> GetById(Guid id);
    }
}