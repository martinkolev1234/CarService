using CarService.Models.Responses;

namespace CarService.BL.Interfaces
{
    public interface ISellCar
    {
        Task<SellCarResult> SellAsync(Guid carId, Guid customerId);
    }
}