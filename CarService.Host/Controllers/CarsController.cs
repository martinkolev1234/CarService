using CarService.BL.Interfaces;
using CarService.Models.Dto;
using CarService.Models.Requests;
using FluentValidation;
using MapsterMapper;
using Microsoft.AspNetCore.Mvc;

namespace CarService.Host.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarsController : ControllerBase
    {
        private readonly ICarCrudService _carCrudService;
        private readonly IMapper _mapper;
        private readonly IValidator<AddCarRequest> _validator;

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
        public async Task<IActionResult> DeleteCarAsync(Guid id)
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

            var result = await _validator.ValidateAsync(carRequest);

            if (!result.IsValid)
            {
                return BadRequest(result.Errors);
            }

            var car = _mapper.Map<Car>(carRequest);

            await _carCrudService.AddCarАsync(car);

            return Ok();
        }
    }
}