using CarService.BL.Interfaces;
using CarService.Models.Dto;
using CarService.Models.Requests;
using FluentValidation;
using FluentValidation.Results;
using MapsterMapper;
using Microsoft.AspNetCore.Mvc;
using System;

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
        public IActionResult DeleteCar(Guid id)
        {
            if (id == Guid.Empty)
            {
                return BadRequest("ID must be a valid Guid.");
            }
            var car = _carCrudService.GetById(id);
            if (car == null)
            {
                return NotFound($"Car with ID {id} not found.");
            }
            _carCrudService.DeleteCar(id);
            return Ok();
        }

        [HttpGet(nameof(GetById))]
        public IActionResult GetById(Guid id)
        {
            if (id == Guid.Empty)
            {
                return BadRequest("ID must be a valid Guid.");
            }

            var car = _carCrudService.GetById(id);

            if (car == null)
            {
                return NotFound($"Car with ID {id} not found.");
            }

            return Ok(car);
        }

        [HttpGet(nameof(GetAll))]
        public IActionResult GetAll()
        {
            var cars = _carCrudService.GetAllCars();
            return Ok(cars);
        }

        [HttpPost]
        public IActionResult AddCar([FromBody] AddCarRequest? carRequest)
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

            _carCrudService.AddCar(car);

            return Ok();
        }
    }
}