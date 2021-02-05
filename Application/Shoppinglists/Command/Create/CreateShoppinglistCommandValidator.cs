using System.Linq;
using FluentValidation;
using MyFridgeListWebapi.Core.Data.Database;
using MyFridgeListWebapi.Properties;

namespace MyFridgeListWebapi.Application.Shoppinglists.Command.Create
{
    public sealed class CreateShoppinglistCommandValidator : AbstractValidator<CreateShoppinglistCommand>
    {
        public CreateShoppinglistCommandValidator(DatabaseContext dbContext)
        {
            RuleFor(request => request.Name)
                .NotEmpty()
                .WithMessage(Resources.ValidationErrorMissingShoppinglistName);

            RuleFor(request => request)
                .Must(request => !dbContext.Shoppinglists
                    .Where(x => x.UserId == request.UserId)
                    .Any(shoppinglist => shoppinglist.Name == request.Name))
                .WithMessage(request => string.Format(Resources.ValidationErrorShoppinglistWithNameStillExists, request.Name));
        }
    }
}
