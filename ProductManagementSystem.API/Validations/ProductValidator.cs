using FluentValidation;
using MongoDB.Bson;
using ProductManagementSystem.Domain.Entities;

namespace ProductManagementSystem.API.Validations;

public class ProductValidator : AbstractValidator<Product>
{
    public ProductValidator()
    {
        RuleFor(x => x.Name)
            .NotNull()
            .NotEmpty()
            .WithMessage("Name is required")
            .MaximumLength(100)
            .WithMessage("Name must be less than 100 characters");

        RuleFor(x => x.Description)
            .NotNull()
            .NotEmpty()
            .WithMessage("Description is required")
            .MaximumLength(150)
            .WithMessage("Description must be less than 150 characters")
            .Must((x, _) => { 
                return x.Description.Contains(x.Name, StringComparison.OrdinalIgnoreCase); 
            }).WithMessage("Description does not contain the name of the product");

        RuleFor(x => x.Price)
            .NotEmpty()
            .NotNull()
            .WithMessage("Price is required");

        RuleFor(x => x.Id)
            .NotEmpty()
            .NotNull()
            .WithMessage("Id is required")
            .Must(id => ObjectId.TryParse(id, out _))
            .WithMessage("Invalid Id. Please ensure it's a valid BSON Id");

        RuleFor(x => x.CategoryId)
            .NotEmpty()
            .NotNull()
            .WithMessage("Category is required")
            .Must(id => ObjectId.TryParse(id, out _))
            .WithMessage("Invalid Id. Please ensure it's a valid BSON Id");

    }
}
