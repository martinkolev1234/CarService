using CarService.Models.Dto;

namespace CarService.BL.Interfaces 
{
    public interface ICarCrudService
    {
        Task AddCar(Car car);
        Task DeleteCar(Guid id); 
        Task<List<Car>> GetAllCars();
        Task<Car?> GetById(Guid id);
    }
}