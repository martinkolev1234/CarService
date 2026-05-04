using System;
using MessagePack;

namespace CarService.Models.Dto
{
    [MessagePackObject]
    public class Car
    {
        [Key(0)]
        public Guid Id { get; set; }

        [Key(1)]
        public string Model { get; set; } = string.Empty;

        [Key(2)]
        public int Year { get; set; }

        [Key(3)]
        public decimal BasePrice { get; set; }
    }
}