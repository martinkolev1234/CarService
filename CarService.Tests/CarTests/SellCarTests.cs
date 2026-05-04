using System;
using System.Threading.Tasks;
using CarService.BL.Interfaces;
using CarService.DL.Interfaces;
using CarService.Models.Dto;
using CarService3.DL.Interfaces; 
using Moq;
using Xunit; 

namespace CarService.Test
{
    public class SellCarTests
    {
        Mock<ICarCrudService> _carCrudServiceMock;
        Mock<ICustomerRepository> _customerRepositoryMock;

        [Fact]
        public async Task Sell_Return_Ok() 
        {

            _carCrudServiceMock = new Mock<ICarCrudService>();
            _customerRepositoryMock = new Mock<ICustomerRepository>();
            var expectedPrice = 24000m;

            _carCrudServiceMock.Setup(x => x.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync(new Car
            {
                Id = Guid.NewGuid(),
                Model = "Camry",
                Year = 2020,
                BasePrice = 25000m
            });

            _customerRepositoryMock.Setup(x => x.GetById(It.IsAny<Guid>())).ReturnsAsync(new Customer
            {
                Id = Guid.NewGuid(),
                Email = "xxx@xxx.com",
                Discount = 1000,
                Name = "John Doe"
            });

            var sellCarService = new BL.Services.SellCar(_carCrudServiceMock.Object, _customerRepositoryMock.Object);
            var result = await sellCarService.Sell(Guid.NewGuid(), Guid.NewGuid());

            Assert.NotNull(result);
            Assert.Equal(expectedPrice, result.Price);
        }

        [Fact]
        public async Task Sell_When_Customer_Missing() 
        {

            _carCrudServiceMock = new Mock<ICarCrudService>();
            _customerRepositoryMock = new Mock<ICustomerRepository>();

            _carCrudServiceMock.Setup(x => x.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync(new Car
            {
                Id = Guid.NewGuid(),
                Model = "Camry",
                Year = 2020,
                BasePrice = 25000m
            });

            _customerRepositoryMock.Setup(x => x.GetById(It.IsAny<Guid>())).ReturnsAsync((Customer?)null);

            var sellCarService = new BL.Services.SellCar(_carCrudServiceMock.Object, _customerRepositoryMock.Object);
            var ex = await Assert.ThrowsAsync<ArgumentException>(() => sellCarService.Sell(Guid.NewGuid(), Guid.NewGuid()));
        }
    }
}