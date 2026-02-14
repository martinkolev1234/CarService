using CarService.BL.Interfaces;
using CarService.DL.Interfaces;
using CarService.Models.Dto;

namespace CarService.BL.Services
{
    internal class CarCrudService : ICarCrudService
    {
        private readonly ICarRepository _carRepository;

        public CarCrudService(ICarRepository carRepository)
        {
            _carRepository = carRepository;
        }

        public void AddCar(Car car)
        {
            if (car == null) return;

            if(car?.Id == null || car.Id == Guid.Empty)
            {
                car!.Id = Guid.NewGuid();
            }

            _carRepository.AddCar(car);
        }

        public void DeleteCar(Guid id)
        {
            _carRepository.DeleteCar(id);
        }

        public List<Car> GetAllCars()
        {
            return _carRepository.GetAllCars();
        }

        public Car? GetById(Guid id)
        {
            return _carRepository.GetById(id);
        }
    }
}
