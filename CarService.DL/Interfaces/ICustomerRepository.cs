using CarService.Models.Dto;

namespace CarService.DL.Interfaces
{
    public interface ICustomerRepository
    {
        Task AddAsync(Customer? customer);
        Task<List<Customer>> GetAll();
        Task<Customer?> GetByIdAsync(Guid id);
        Task DeleteAsync(Guid id);
    }
}