using FluentValidation;
using MyFridgeListWebapi.Properties;

namespace MyFridgeListWebapi.Application.Items.Queries.All
{
    public sealed class GetItemsQueryValidator : AbstractValidator<GetItemsQuery>
    {
        public GetItemsQueryValidator()
        {
            RuleFor(request => request.ShoppinglistId)
                .NotEmpty()
                .WithMessage(request => string.Format(Resources.ValidationErrorMissingShoppinglistId));
        }
    }
}
