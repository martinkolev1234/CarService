using CarService.Models.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
