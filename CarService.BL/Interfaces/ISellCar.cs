using CarService.Models.Responses;

namespace CarService.BL.Interfaces
{
    internal interface ISellCar
    {
        SellCarResult Sell(Guid carId, Guid customerId);
    }
}
