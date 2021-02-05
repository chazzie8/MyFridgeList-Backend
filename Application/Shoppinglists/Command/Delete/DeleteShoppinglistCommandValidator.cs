using FluentValidation;
using MyFridgeListWebapi.Properties;

namespace MyFridgeListWebapi.Application.Shoppinglists.Command.Delete
{
    public class DeleteShoppinglistCommandValidator : AbstractValidator<DeleteShoppinglistCommand>
    {
        public DeleteShoppinglistCommandValidator()
        {
            RuleFor(request => request.ShoppinglistId)
                .NotEmpty()
                .WithMessage(Resources.ValidationErrorMissingShoppinglistId);
        }
    }
}
