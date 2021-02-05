using FluentValidation;
using MyFridgeListWebapi.Properties;

namespace MyFridgeListWebapi.Application.Shoppinglists.Command.Edit
{
    public sealed class EditShoppinglistCommandValidator : AbstractValidator<EditShoppinglistCommand>
    {
        public EditShoppinglistCommandValidator()
        {
            RuleFor(request => request.ShoppinglistId)
                .NotEmpty()
                .WithMessage(Resources.ValidationErrorMissingShoppinglistId);
        }
    }
}
