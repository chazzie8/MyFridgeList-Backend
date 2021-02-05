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

            RuleFor(request => request)
                .Must(request => !dbContext.Fridges
                    .Where(x => x.UserId == request.UserId)
                    .Any(fridge => fridge.Name == request.Name))
                .WithMessage(request => string.Format(Resources.ValidationErrorFridgeWithNameStillExists, request.Name));
        }
    }
}
