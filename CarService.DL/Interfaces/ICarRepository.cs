using CarService.Models.Dto;

namespace CarService.DL.Interfaces
{
    public interface ICarRepository
    {
        Task AddCar(Car car);
        Task DeleteCar(Guid? id); 
        Task<List<Car>> GetAllCars();
        Task<Car?> GetById(Guid? id);
    }
}