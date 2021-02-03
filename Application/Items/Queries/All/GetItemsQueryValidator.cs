using System.Linq;
using FluentValidation;
using MyFridgeListWebapi.Core.Data.Database;
using MyFridgeListWebapi.Properties;

namespace MyFridgeListWebapi.Application.Items.Queries.All
{
    public sealed class GetItemsQueryValidator : AbstractValidator<GetItemsQuery>
    {
        public GetItemsQueryValidator(DatabaseContext dbContext)
        {
            RuleFor(request => request.ShoppinglistId)
                .Must(id => dbContext.Shoppinglists.Any(shoppinglist => shoppinglist.Id == id))
                .WithMessage(request => string.Format(Resources.ValidationErrorShoppinglistWithIdNotExists, request.ShoppinglistId));
        }
    }
}
