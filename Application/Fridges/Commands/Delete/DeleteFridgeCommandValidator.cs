using FluentValidation;
using MyFridgeListWebapi.Properties;

namespace MyFridgeListWebapi.Application.Fridges.Commands.Delete
{
    public sealed class DeleteFridgeCommandValidator : AbstractValidator<DeleteFridgeCommand>
    {
        public DeleteFridgeCommandValidator()
        {
            RuleFor(request => request.FridgeId)
                .NotEmpty()
                .WithMessage(Resources.ValidationErrorMissingFridgeId);
        }
    }
}
