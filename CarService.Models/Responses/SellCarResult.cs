using System;
using MessagePack;
using CarService.Models.Dto;

namespace CarService.Models.Responses
{
    [MessagePackObject]
    public class SellCarResult
    {
        [Key(0)]
        public Car Car { get; set; }

        [Key(1)]
        public Customer Customer { get; set; }

        [Key(2)]
        public decimal Price { get; set; }
    }
}