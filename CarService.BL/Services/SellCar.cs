using System;
using System.Threading.Tasks;
using CarService.BL.Interfaces;
using CarService.DL.Interfaces;
using CarService.DL.Kafka;
using CarService.Models.Responses;
using CarService3.DL.Interfaces;

namespace CarService.BL.Services
{
    internal class SellCar : ISellCar
    {
        private readonly ICarCrudService _carCrudService;
        private readonly ICustomerRepository _customerRepository;
        private readonly GenericKafkaProducer<string, SellCarResult> _producer;

        public SellCar(
            ICarCrudService carCrudService,
            ICustomerRepository customerRepository,
            GenericKafkaProducer<string, SellCarResult> producer)
        {
            _carCrudService = carCrudService;
            _customerRepository = customerRepository;
            _producer = producer;
        }

        public async Task<SellCarResult> Sell(Guid carId, Guid customerId)
        {
            var car = await _carCrudService.GetByIdAsync(carId);
            var customer = await _customerRepository.GetById(customerId);

            if (car == null || customer == null)
                throw new ArgumentException("Car or Customer not found.");

            var price = car.BasePrice - customer.Discount;

            var result = new SellCarResult
            {
                Price = price,
                Car = car,
                Customer = customer
            };

            // PRODUCE SellCarResult directly
            await _producer.ProduceAsync(carId.ToString(), result);

            return result;
        }
    }
}