using FluentValidation;
using MyFridgeListWebapi.Properties;

namespace MyFridgeListWebapi.Application.Shoppinglists.Command.Update
{
    public sealed class UpdateItemsCommandValidator : AbstractValidator<UpdateItemsCommand>
    {
        public UpdateItemsCommandValidator()
        {
            RuleFor(request => request.ShoppinglistId)
                .NotEmpty()
                .WithMessage(Resources.ValidationErrorMissingShoppinglistId);
        }
    }
}
