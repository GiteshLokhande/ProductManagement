using ProductManagement.Application.DTOs;
using FluentValidation;

namespace ProductManagement.Application.Validators
{
    public class UpdateProductValidator : AbstractValidator<UpdateProductDto>
    {
        public UpdateProductValidator()
        {
            RuleFor(x => x.ProductName)
                .NotEmpty()
                .WithMessage("Product Name is required.")
                .MaximumLength(255)
                .WithMessage("Product Name cannot exceed 255 characters.");
        }
    }
}
