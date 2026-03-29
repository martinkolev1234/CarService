using CarService.Models.Dto;

namespace CarService.DL.Interfaces
{
    public interface ICarRepository
    {
        void AddCar(Car car);

        void DeleteCar(Guid? id);

        List<Car> GetAllCars();

        Car? GetById(Guid? id);
    }
}
