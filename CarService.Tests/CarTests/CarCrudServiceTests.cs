using CarService.BL.Services;
using CarService.DL.Interfaces;
using CarService.Models.Dto;
using CarService.Tests.MockData;
using Moq;

namespace CarService.Tests.CarTests
{
    public class CarCrudServiceTests
    {
        private readonly Mock<ICarRepository> _carRepositoryMock;

        public CarCrudServiceTests()
        {
            _carRepositoryMock = new Mock<ICarRepository>();
        }

        [Fact]
        public async Task AddCarTest_Ok() 
        {
            var expectedCarCount = CarMockedData.Cars.Count + 1;
            var id = Guid.NewGuid();
            var car = new Car()
            {
                Id = id,
                Model = "Camry",
                Year = 2020
            };

            _carRepositoryMock
                .Setup(repo => repo.AddCarAsync(car))
                .Returns(Task.CompletedTask) 
                .Callback(() =>
                {
                    CarMockedData.Cars.Add(car);
                });

            var service = new CarCrudService(_carRepositoryMock.Object);

            await service.AddCarAsync(car);

            var resultCar = CarMockedData.Cars.FirstOrDefault(c => c.Id == id);

            Assert.NotNull(resultCar);
            Assert.Contains(car, CarMockedData.Cars);
            Assert.Equal(expectedCarCount, CarMockedData.Cars.Count);
            Assert.Equal(id, resultCar.Id);
        }
    }
}