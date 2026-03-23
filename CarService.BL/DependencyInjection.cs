using CarService.BL.Interfaces;
using CarService.BL.Services;
using Microsoft.Extensions.DependencyInjection;

namespace CarService.BL
{
    public static class DependencyInjection
    {
        public static IServiceCollection
            AddBusinessLayer(this IServiceCollection services)
        {
            services.AddSingleton<ICarCrudService, CarCrudService>();
            services.AddSingleton<ISellCar, SellCar>();
            services.AddSingleton<ICustomerCrudService,
                CustomerCrudService>();

            return services;
        }
    }
}
