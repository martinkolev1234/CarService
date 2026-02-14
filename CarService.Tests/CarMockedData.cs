using CarService.Models.Dto;

namespace CarService.Tests.MockData
{
    internal class CarMockedData
    {
        public static List<Car> Cars = new List<Car>
        {
        new Car { Id = Guid.NewGuid(), Model = "Toyota Camry", Year = 2020 },
        new Car { Id = Guid.NewGuid(), Model = "Honda Accord", Year = 2019 },
        new Car { Id = Guid.NewGuid(), Model = "Ford Mustang", Year = 2021 }
            };
    }
}
