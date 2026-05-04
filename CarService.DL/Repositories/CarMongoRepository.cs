using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CarService.DL.Interfaces;
using CarService.Models.Configurations;
using CarService.Models.Dto;
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

        public async Task AddCarAsync(Car car)
        {
            if (car == null) return;

            try
            {
                await _carsCollection.InsertOneAsync(car);
            }
            catch (Exception e)
            {
                _logger.LogError("Error adding car to the DB:{0}-{1}", e.Message, e.StackTrace);
            }
        }

        public async Task DeleteCarAsync(Guid? id)
        {
            if (id == null || id == Guid.Empty) return;

            try
            {
                var result = await _carsCollection.DeleteOneAsync(c => c.Id == id);

                if (result.DeletedCount == 0)
                {
                    _logger.LogWarning($"No car found with Id: {id} to delete.");
                }
            }
            catch (Exception e)
            {
                _logger.LogError($"Error in method {nameof(DeleteCarAsync)}:{e.Message}-{e.StackTrace}");
            }
        }

        public async Task<List<Car>> GetAllCarsAsync()
        {
            return await _carsCollection.Find(_ => true).ToListAsync();
        }

        public async Task<Car?> GetByIdAsync(Guid? id)
        {
            if (id == null || id == Guid.Empty) return default;

            try
            {
                return await _carsCollection.Find(c => c.Id == id).FirstOrDefaultAsync();
            }
            catch (Exception e)
            {
                _logger.LogError($"Error in method {nameof(GetByIdAsync)}:{e.Message}-{e.StackTrace}");
            }

            return default;
        }
    }
}