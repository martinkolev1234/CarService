using CarService.BL.Interfaces;
using CarService.Models.Dto;
using CarService.Models.Requests;
using FluentValidation;
using FluentValidation.Results;
using MapsterMapper;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks; 

namespace CarService.Host.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarsController : ControllerBase
    {
        private readonly ICarCrudService _carCrudService;
        private readonly IMapper _mapper;
        private IValidator<AddCarRequest> _validator;

        public CarsController(
            ICarCrudService carCrudService,
            IMapper mapper,
            IValidator<AddCarRequest> validator)
        {
            _carCrudService = carCrudService;
            _mapper = mapper;
            _validator = validator;
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteCar(Guid id)
        {
            if (id == Guid.Empty)
            {
                return BadRequest("ID must be a valid Guid.");
            }

            var car = await _carCrudService.GetByIdAsync(id);
            if (car == null)
            {
                return NotFound($"Car with ID {id} not found.");
            }

            await _carCrudService.DeleteCarAsync(id);
            return Ok();
        }

        [HttpGet(nameof(GetById))]
        public async Task<IActionResult> GetById(Guid id)
        {
            if (id == Guid.Empty)
            {
                return BadRequest("ID must be a valid Guid.");
            }

            var car = await _carCrudService.GetByIdAsync(id);

            if (car == null)
            {
                return NotFound($"Car with ID {id} not found.");
            }

            return Ok(car);
        }

        [HttpGet(nameof(GetAll))]
        public async Task<IActionResult> GetAll()
        {

            var cars = await _carCrudService.GetAllCarsAsync();
            return Ok(cars);
        }

        [HttpPost]
        public async Task<IActionResult> AddCar([FromBody] AddCarRequest? carRequest)
        {
            if (carRequest == null)
            {
                return BadRequest("Car data is null.");
            }

            var result = _validator.Validate(carRequest);

            if (!result.IsValid)
            {
                return BadRequest(result.Errors);
            }

            var car = _mapper.Map<Car>(carRequest);

            await _carCrudService.AddCarAsync(car);

            return Ok();
        }
    }
}