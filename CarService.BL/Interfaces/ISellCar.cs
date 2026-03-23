using CarService.Models.Responses;

namespace CarService.BL.Interfaces
{
    internal interface ISellCar
    {
        Task<SellCarResult> Sell(Guid carId, Guid customerId);
    }
}