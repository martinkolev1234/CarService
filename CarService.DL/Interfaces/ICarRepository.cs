using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CarService.Models.Dto;

namespace CarService.DL.Interfaces
{
    public interface ICarRepository
    {
        Task AddCarAsync(Car car);

        Task DeleteCarAsync(Guid? id);

        Task<List<Car>> GetAllCarsAsync();

        Task<Car?> GetByIdAsync(Guid? id);
    }
}