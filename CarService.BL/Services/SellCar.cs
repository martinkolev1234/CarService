using System;
using System.Threading.Tasks;
using CarService.BL.Interfaces;
using CarService.DL.Interfaces;
using CarService.Models.Responses;
using CarService3.DL.Interfaces;

namespace CarService.BL.Services
{
    internal class SellCar : ISellCar
    {
        private readonly ICarCrudService _carCrudService;
        private readonly ICustomerRepository _customerRepository;

        public SellCar(ICarCrudService carCrudService, ICustomerRepository customerRepository)
        {
            _carCrudService = carCrudService;
            _customerRepository = customerRepository;
        }

        public async Task<SellCarResult> Sell(Guid carId, Guid customerId)
        {

            var car = await _carCrudService.GetByIdAsync(carId);

            var customer = await _customerRepository.GetById(customerId);

            if (car == null || customer == null)
            {
                throw new ArgumentException($"Car or Customer not found.");
            }

            var price = car.BasePrice - customer.Discount;

            return new SellCarResult
            {
                Price = price,
                Car = car, 
                Customer = customer
            };
        }
    }
}