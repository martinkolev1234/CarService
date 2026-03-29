using CarService.Models.Dto;

namespace CarService3.BL.Interfaces
{
    public interface ICustomerCrudService
    {
        Task Add(Customer? customer);
        Task<List<Customer>> GetAll();
        Task<Customer?> GetById(Guid id);
        Task Delete(Guid id);
    }
}