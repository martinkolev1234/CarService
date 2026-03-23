using CarService.DL.Interfaces;
using CarService.DL.LocalDb;
using CarService.Models.Dto;

namespace CarService.DL.Repositories
{
    [Obsolete($"Please use: {nameof(CarMongoRepository)}")]
    internal class CarLocalRepository : ICarRepository
    {
        public Task AddCar(Car car)
        {
            StaticDb.Cars.Add(car);
            return Task.CompletedTask;
        }

        public Task DeleteCar(Guid? id)
        {
            StaticDb.Cars.RemoveAll(c => c.Id == id);
            return Task.CompletedTask;
        }

        public Task<List<Car>> GetAllCars()
        {
            return Task.FromResult(StaticDb.Cars);
        }

        public Task<Car?> GetById(Guid? id)
        {
            return Task.FromResult(StaticDb.Cars.FirstOrDefault(c => c.Id == id));
        }
    }
}