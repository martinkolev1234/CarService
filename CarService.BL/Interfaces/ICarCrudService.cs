using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CarService.Models.Dto;

namespace CarService.BL.Interfaces
{
    public interface ICarCrudService
    {
        Task AddCarAsync(Car car);

        Task DeleteCarAsync(Guid id);

        Task<List<Car>> GetAllCarsAsync();

        Task<Car?> GetByIdAsync(Guid id);
    }
}