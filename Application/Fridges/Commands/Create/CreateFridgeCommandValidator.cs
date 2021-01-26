using System.Linq;
using FluentValidation;
using MyFridgeListWebapi.Core.Data.Database;
using MyFridgeListWebapi.Properties;

namespace MyFridgeListWebapi.Application.Fridges.Commands.Create
{
    public sealed class CreateFridgeCommandValidator : AbstractValidator<CreateFridgeCommand>
    {
        public CreateFridgeCommandValidator(DatabaseContext dbContext)
        {
            RuleFor(request => request.Name)
                .NotEmpty()
                .WithMessage(Resources.ValidationErrorMissingFridgeName);

            RuleFor(request => request.Name)
                .Must(name => !dbContext.Fridges.Any(fridge => fridge.Name == name))
                .WithMessage(request => string.Format(Resources.ValidationErrorFridgeWithNameStillExists, request.Name));
        }
    }
}
