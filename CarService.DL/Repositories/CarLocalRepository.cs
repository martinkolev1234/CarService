using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarService.DL.Interfaces;
using CarService.DL.LocalDb;
using CarService.Models.Dto;

namespace CarService.DL.Repositories
{
    [Obsolete($"Please use: {nameof(CarMongoRepository)}")]
    internal class CarLocalRepository : ICarRepository
    {
        public Task AddCarAsync(Car car)
        {
            StaticDb.Cars.Add(car);
            return Task.CompletedTask;
        }

        public Task DeleteCarAsync(Guid? id)
        {
            StaticDb.Cars.RemoveAll(c => c.Id == id);
            return Task.CompletedTask;
        }

        public Task<List<Car>> GetAllCarsAsync()
        {
            return Task.FromResult(StaticDb.Cars);
        }

        public Task<Car?> GetByIdAsync(Guid? id)
        {
            var car = StaticDb.Cars.FirstOrDefault(c => c.Id == id);
            return Task.FromResult(car);
        }
    }
}