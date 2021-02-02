using System.Linq;
using FluentValidation;
using MyFridgeListWebapi.Core.Data.Database;
using MyFridgeListWebapi.Properties;

namespace MyFridgeListWebapi.Application.Items.Commands.Delete
{
    public class DeleteItemCommandValidator : AbstractValidator<DeleteItemCommand>
    {
        public DeleteItemCommandValidator(DatabaseContext dbContext)
        {
            RuleFor(request => request.ShoppinglistId)
                .Must(id => dbContext.Shoppinglists.Any(shoppinglist => shoppinglist.Id == id))
                .WithMessage(request => string.Format(Resources.ValidationErrorShoppinglistWithIdNotExists, request.ShoppinglistId));

            RuleFor(request => request.ItemId)
                .Must(id => dbContext.Items.Any(item => item.Id == id))
                .WithMessage(request => string.Format(Resources.ValidationErrorItemWithIdNotExists, request.ItemId));
        }
    }
}
