using CarService.Host.Controllers;
using CarService.Models.Dto;
using FluentValidation;

namespace CarService.Host.Validators
{
    public class AddCustomerRequestValidator : AbstractValidator<Customer>
    {
            public AddCustomerRequestValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("Id cannot be empty.");

            // Name Validation
            RuleFor(x => x.Name)
                .NotNull()
                .NotEmpty()
                .MaximumLength(100).WithMessage("Name cannot exceed 100 characters.")
                .MinimumLength(2).WithMessage("Name must be at least 2 characters.")
                .WithMessage("Name is required.");

            // Email Validation
            RuleFor(x => x.Email)
                .NotNull()
                .NotEmpty().WithMessage("Email is required.")
                .EmailAddress().WithMessage("A valid email address is required.");
        }
        }
    }

