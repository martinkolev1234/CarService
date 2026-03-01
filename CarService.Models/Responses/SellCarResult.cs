using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarService.Models.Dto;

namespace CarService.Models.Responses
{
    public class SellCarResult
    {
        public Car Car { get; set; }

        public Customer Customer { get; set; }

        public decimal Price { get; set; }
    }
}
