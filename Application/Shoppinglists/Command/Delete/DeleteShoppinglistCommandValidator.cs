using System.Linq;
using FluentValidation;
using MyFridgeListWebapi.Core.Data.Database;
using MyFridgeListWebapi.Properties;

namespace MyFridgeListWebapi.Application.Shoppinglists.Command.Delete
{
    public class DeleteShoppinglistCommandValidator : AbstractValidator<DeleteShoppinglistCommand>
    {
        public DeleteShoppinglistCommandValidator(DatabaseContext dbContext)
        {
            RuleFor(request => request.ShoppinglistId)
                .NotEmpty()
                .WithMessage(Resources.ValidationErrorMissingShoppinglistId);

            RuleFor(request => request.ShoppinglistId)
                .Must(id => dbContext.Shoppinglists.Any(shoppinglist => shoppinglist.Id == id))
                .WithMessage(request => string.Format(Resources.ValidationErrorShoppinglistWithIdNotExists, request.ShoppinglistId));
        }
    }
}
