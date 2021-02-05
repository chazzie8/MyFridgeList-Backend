using FluentValidation;
using MyFridgeListWebapi.Properties;

namespace MyFridgeListWebapi.Application.Items.Commands.Create
{
    public sealed class CreateItemCommandValidator : AbstractValidator<CreateItemCommand>
    {
        public CreateItemCommandValidator()
        {
            RuleFor(request => request.ShoppinglistId)
                .NotEmpty()
                .WithMessage(Resources.ValidationErrorMissingShoppinglistId);

            RuleFor(request => request.Label)
                .NotEmpty()
                .WithMessage(Resources.ValidationErrorMissingItemName);
        }
    }
}
