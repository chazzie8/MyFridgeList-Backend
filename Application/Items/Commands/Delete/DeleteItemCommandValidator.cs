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
                .NotEmpty()
                .WithMessage(request => string.Format(Resources.ValidationErrorMissingShoppinglistId));

            RuleFor(request => request.ItemId)
                .NotEmpty()
                .WithMessage(request => string.Format(Resources.ValidationErrorMissingItemId));
        }
    }
}
