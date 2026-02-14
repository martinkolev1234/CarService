using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CarService.BL.Interfaces;
using CarService.DL.Interfaces;
using CarService.Models.Dto;
using Moq;

namespace CarService.Test
{
    public class SellCarTests
    {
        Mock<ICarCrudService> _carCrudServiceMock;
        Mock<ICustomerRepository> _customerRepositoryMock;


        [Fact]
        public void Sell_Return_Ok()
        {
            //arrange
            _carCrudServiceMock = new Mock<ICarCrudService>();
            _customerRepositoryMock = new Mock<ICustomerRepository>();
            var expectedPrice = 24000m;

            _carCrudServiceMock.Setup(x => x.GetById(It.IsAny<Guid>())).Returns(new Models.Dto.Car
            {
                Id = Guid.NewGuid(),
                Model = "Camry",
                Year = 2020,
                BasePrice = 25000m
            });

            _customerRepositoryMock.Setup(x => x.GetById(It.IsAny<Guid>())).Returns(new Models.Dto.Customer
            {
                Id = Guid.NewGuid(),
                Email = "xxx@xxx.com",
                Discount = 1000,
                Name = "John Doe"
            });

            var sellCarService = new BL.Services.SellCar(_carCrudServiceMock.Object, _customerRepositoryMock.Object);

            //act
            var result = sellCarService.Sell(Guid.NewGuid(), Guid.NewGuid());

            //assert
            Assert.NotNull(result);
            Assert.Equal(expectedPrice, result.Price);
        }

        [Fact]
        public void Sell_When_Customer_Missing()
        {
            //arrange
            _carCrudServiceMock = new Mock<ICarCrudService>();
            _customerRepositoryMock = new Mock<ICustomerRepository>();
            var expectedPrice = 24000m;

            _carCrudServiceMock.Setup(x => x.GetById(It.IsAny<Guid>())).Returns(new Models.Dto.Car
            {
                Id = Guid.NewGuid(),
                Model = "Camry",
                Year = 2020,
                BasePrice = 25000m
            });

            _customerRepositoryMock.Setup(x => x.GetById(It.IsAny<Guid>())).Returns((Customer)null);

            var sellCarService = new BL.Services.SellCar(_carCrudServiceMock.Object, _customerRepositoryMock.Object);

            //act + Assert
            var ex = Assert.Throws<ArgumentException>(() => sellCarService.Sell(Guid.NewGuid(), Guid.NewGuid()));
        }

    }
}

