using System.Linq;
using FluentValidation;
using MyFridgeListWebapi.Core.Data.Database;
using MyFridgeListWebapi.Properties;

namespace MyFridgeListWebapi.Application.Items.Commands.Create
{
    public sealed class CreateItemCommandValidator : AbstractValidator<CreateItemCommand>
    {
        public CreateItemCommandValidator(DatabaseContext dbContext)
        {
            RuleFor(request => request.ShoppinglistId)
                .Must(id => dbContext.Shoppinglists.Any(shoppinglist => shoppinglist.Id == id))
                .WithMessage(request => string.Format(Resources.ValidationErrorShoppinglistWithIdNotExists, request.ShoppinglistId));

            RuleFor(request => request.Label)
                .NotEmpty()
                .WithMessage(Resources.ValidationErrorMissingItemName);
        }
    }
}
