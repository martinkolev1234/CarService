using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarService.Models.Requests
{
    public class AddCarRequest
    {
        public string Model { get; set; } = string.Empty;
        public int Year { get; set; }
    }
}
