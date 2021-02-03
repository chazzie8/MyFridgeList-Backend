using System.Linq;
using FluentValidation;
using MyFridgeListWebapi.Core.Data.Database;
using MyFridgeListWebapi.Properties;

namespace MyFridgeListWebapi.Application.Articles.Queries.All
{
    public sealed class GetArticlesQueryValidator : AbstractValidator<GetArticlesQuery>
    {
        public GetArticlesQueryValidator(DatabaseContext dbContext)
        {
            RuleFor(request => request.FridgeId)
                .Must(id => dbContext.Fridges.Any(fridge => fridge.Id == id))
                .WithMessage(request => string.Format(Resources.ValidationErrorFridgeWithIdNotExists, request.FridgeId));
        }
    }
}
