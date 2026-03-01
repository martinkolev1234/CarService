using CarService.Models.Dto;

namespace CarService.DL.LocalDb
{
    internal static class StaticDb
    {
        public static List<Car> Cars = new List<Car>
        {
            new Car { Id = Guid.NewGuid(), Model = "Toyota Camry", Year = 2020 },
            new Car { Id = Guid.NewGuid(), Model = "Honda Accord", Year = 2019 },
            new Car { Id = Guid.NewGuid(), Model = "Ford Mustang", Year = 2021 }
        };

        public static List<Customer> Customers =
            new List<Customer>()
            {
                new Customer()
                {
                    Id = Guid.NewGuid(),
                    Name = "John Doe",
                    Email = "jd@xxx.com"
                },
                new Customer()
                {
                    Id = Guid.NewGuid(),
                    Name = "Stamat Genov",
                    Email = "sg@xxx.com"
                }
            };
    }
}
