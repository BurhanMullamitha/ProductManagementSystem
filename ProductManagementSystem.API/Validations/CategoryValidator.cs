using FluentValidation;
using MongoDB.Bson;
using ProductManagementSystem.Domain.Entities;

namespace ProductManagementSystem.API.Validations
{
    public class CategoryValidator: AbstractValidator<Category>
    {
        public CategoryValidator() 
        {
            RuleFor(x => x.Id)
            .NotEmpty()
            .NotNull()
            .WithMessage("Id is required")
            .Must(id => ObjectId.TryParse(id, out _))
            .WithMessage("Invalid Id. Please ensure it's a valid BSON Id");

            RuleFor(x => x.Name)
            .NotNull()
            .NotEmpty()
            .WithMessage("Name is required");

            RuleFor(x => x.Description)
            .NotNull()
            .NotEmpty()
            .WithMessage("Description is required");

        }
    }
}
