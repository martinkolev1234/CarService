using CarService.BL.Interfaces;
using CarService3.BL.Services;
using CarService3.BL.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using CarService.BL.Services;

namespace CarService.BL
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddBusinessLayer(this IServiceCollection services)
        {
            services.AddSingleton<ICarCrudService, CarCrudService>();
            services.AddSingleton<ISellCar, SellCar>();
            services.AddSingleton<ICustomerCrudService, CustomerService>();

            return services;
        }
    }
}