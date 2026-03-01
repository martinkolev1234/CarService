using CarService.BL.Interfaces;
using CarService.DL.Interfaces;
using CarService.Models.Responses;


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

        public SellCarResult Sell(Guid carId, Guid customerId)
        {
            var car = _carCrudService.GetById(carId);

            var customer = _customerRepository.GetById(customerId);

            if (car == null || customer == null)
            {
                throw new ArgumentException($"Car with ID {carId} not found.");
            }

            var price = car.BasePrice - customer.Discount; // Logic to determine price goes here

            return new SellCarResult
            {
                Price = price,
                Car = car,
                Customer = customer
            };
        }

    }
}



