using System.Linq;
using FluentValidation;
using MyFridgeListWebapi.Core.Data.Database;
using MyFridgeListWebapi.Properties;

namespace MyFridgeListWebapi.Application.Fridges.Commands.Delete
{
    public sealed class DeleteFridgeCommandValidator : AbstractValidator<DeleteFridgeCommand>
    {
        public DeleteFridgeCommandValidator(DatabaseContext dbContext)
        {
            RuleFor(request => request.FridgeId)
                .NotEmpty()
                .WithMessage(Resources.ValidationErrorMissingFridgeId);

            RuleFor(request => request.FridgeId)
                .Must(id => dbContext.Fridges.Any(fridge => fridge.Id == id))
                .WithMessage(request => string.Format(Resources.ValidationErrorFridgeWithIdNotExists, request.FridgeId));
        }
    }
}
