using CarService.DL.Interfaces;
using CarService.Models.Configurations;
using CarService.Models.Dto;
using DnsClient.Internal;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace CarService.DL.Repositories
{
    internal class CarMongoRepository : ICarRepository
    {
        private readonly IOptionsMonitor<MongoDbConfiguration> _mongoDbConfiguration;
        private readonly ILogger<CarMongoRepository> _logger;
        private readonly IMongoCollection<Car> _carsCollection;

        public CarMongoRepository(
            IOptionsMonitor<MongoDbConfiguration> mongoDbConfiguration,
            ILogger<CarMongoRepository> logger)
        {
            _mongoDbConfiguration = mongoDbConfiguration;
            _logger = logger;

            var client = new MongoClient(_mongoDbConfiguration.CurrentValue.ConnectionString);

            var database = client.GetDatabase(_mongoDbConfiguration.CurrentValue.DatabaseName);

            _carsCollection = database.GetCollection<Car>($"{nameof(Car)}s");
        }

        public void AddCar(Car car)
        {
            if (car == null) return;

            try
            {
                _carsCollection.InsertOne(car);
            }
            catch (Exception e)
            {
                _logger.LogError("Error adding car to the DB:{0}-{1}", e.Message, e.StackTrace);
            }
        }

        public void DeleteCar(Guid? id)
        {
            if (id == null || id == Guid.Empty) return;

            try
            {
                var result = _carsCollection.DeleteOne(c => c.Id == id);

                if (result.DeletedCount == 0)
                {
                    _logger.LogWarning($"No car found with Id: {id} to delete.");
                }
            }
            catch (Exception e)
            {
                _logger.LogError($"Error in method {nameof(DeleteCar)}:{e.Message}-{e.StackTrace}");
            }
        }

        public List<Car> GetAllCars()
        {
            return _carsCollection.Find(_ => true).ToList();
        }

        public Car? GetById(Guid? id)
        {
            if (id == null || id == Guid.Empty) return default;

            try
            {
                return _carsCollection.Find(c => c.Id == id).FirstOrDefault();
            }
            catch (Exception e)
            {
                _logger.LogError($"Error in method {nameof(GetById)}:{e.Message}-{e.StackTrace}");
            }

            return default;
        }
    }
}