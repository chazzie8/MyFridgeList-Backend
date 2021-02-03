using System.Linq;
using FluentValidation;
using MyFridgeListWebapi.Core.Data.Database;
using MyFridgeListWebapi.Properties;

namespace MyFridgeListWebapi.Application.Articles.Commands.Delete
{
    public sealed class DeleteArticleCommandValidator : AbstractValidator<DeleteArticleCommand>
    {
        public DeleteArticleCommandValidator(DatabaseContext dbContext)
        {
            RuleFor(request => request.FridgeId)
                .Must(id => dbContext.Fridges.Any(fridge => fridge.Id == id))
                .WithMessage(request => string.Format(Resources.ValidationErrorFridgeWithIdNotExists, request.FridgeId));

            RuleFor(request => request.ArticleId)
                .Must(id => dbContext.Articles.Any(article => article.Id == id))
                .WithMessage(request => string.Format(Resources.ValidationErrorArticleWithIdNotExists, request.ArticleId));
        }
    }
}
