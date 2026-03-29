namespace CarService.Models.Requests
{
    public class AddCustomerRequest
    {
        public required string Name { get; set; }

        public required string Email { get; set; }

        public int Years { get; set; }
    }
}