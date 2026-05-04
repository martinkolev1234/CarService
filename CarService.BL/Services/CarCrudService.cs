using System;
using System.Collections.Generic;
using System.Threading.Tasks;
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

        public async Task AddCarAsync(Car car)
        {
            if (car == null) return;

            if (car.Id == Guid.Empty)
            {
                car.Id = Guid.NewGuid();
            }

            await _carRepository.AddCarAsync(car);
        }

        public async Task DeleteCarAsync(Guid id)
        {
            await _carRepository.DeleteCarAsync(id);
        }

        public async Task<List<Car>> GetAllCarsAsync()
        {
            return await _carRepository.GetAllCarsAsync();
        }

        public async Task<Car?> GetByIdAsync(Guid id)
        {
            return await _carRepository.GetByIdAsync(id);
        }
    }
}