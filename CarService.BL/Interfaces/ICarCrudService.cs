using CarService.Models.Dto;

namespace CarService.BL.Interfaces
{
    public interface ICarCrudService
    {
        Task AddCarАsync(Car car);
        Task DeleteCarAsync(Guid id);
        Task<List<Car>> GetAllCarsAsync();
        Task<Car?> GetByIdAsync(Guid id);
    }
}