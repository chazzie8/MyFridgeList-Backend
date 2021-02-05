using FluentValidation;
using MyFridgeListWebapi.Properties;

namespace MyFridgeListWebapi.Application.Fridges.Commands.Edit
{
    public sealed class EditFridgeCommandValidator : AbstractValidator<EditFridgeCommand>
    {
        public EditFridgeCommandValidator()
        {
            RuleFor(request => request.Name)
                .NotEmpty()
                .WithMessage(Resources.ValidationErrorMissingFridgeName);

            RuleFor(request => request.FridgeId)
                .NotEmpty()
                .WithMessage(Resources.ValidationErrorMissingFridgeId);
        }
    }
}
