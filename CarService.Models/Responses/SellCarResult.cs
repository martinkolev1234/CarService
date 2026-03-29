using CarService.Models.Dto;

namespace CarService.Models.Responses
{
    public class SellCarResult
    {
        public required Car Car { get; set; }

        public required Customer Customer { get; set; }

        public decimal Price { get; set; }
    }
}