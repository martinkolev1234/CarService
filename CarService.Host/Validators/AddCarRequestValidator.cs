using CarService.Models.Requests;
using FluentValidation;

namespace CarService.Host.Validators
{
    public class AddCarRequestValidator : AbstractValidator<AddCarRequest>
    {
        public AddCarRequestValidator()
        {
            RuleFor(x => x.Model)
                .NotNull()
                .NotEmpty()
                .MaximumLength(5).WithMessage("Model cannot exceed 50 characters.")
                .MinimumLength(2).WithMessage("Model cannot be below 2 characters.")
                .WithMessage("Model is required.");

            RuleFor(x => x.Year)
                .InclusiveBetween(1886, DateTime.Now.Year + 1)
                .WithMessage($"Year must be between 1886 and {DateTime.Now.Year + 1}.");
        }
    }
}
