using MessagePack;

namespace CarService.DL.Kafka
{
    [MessagePackObject]
    public class SellCarMessage
    {
        [Key(0)]
        public Guid CarId { get; set; }

        [Key(1)]
        public Guid CustomerId { get; set; }

        [Key(2)]
        public decimal Price { get; set; }
    }
}