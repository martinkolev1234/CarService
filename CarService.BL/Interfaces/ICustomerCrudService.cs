using CarService.Models.Dto;

namespace CarService.BL.Interfaces
{
    public interface ICustomerCrudService
    {
        Task AddAsync(Customer? customer);
        Task<List<Customer>> GetAllAsync();
        Task<Customer?> GetByIdAsync(Guid id);
        Task DeleteAsync(Guid id);
    }
}