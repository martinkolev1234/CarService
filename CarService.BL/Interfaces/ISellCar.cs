using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarService.Models.Responses;

namespace CarService.BL.Interfaces
{
    internal interface ISellCar
    {
        SellCarResult Sell(Guid carId, Guid customerId);
    }
}
