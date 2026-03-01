using CarService.Models.Dto;

namespace CarService.DL.Interfaces
{
    public interface ICustomerRepository
    {
        void AddCustomer(Customer customer);
        void DeleteCustomer(Guid id);
        List<Customer> GetAllCustomers();
        Customer? GetById(Guid id);
    }
}
