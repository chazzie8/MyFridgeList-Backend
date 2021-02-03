using System.Linq;
using FluentValidation;
using MyFridgeListWebapi.Core.Data.Database;
using MyFridgeListWebapi.Properties;

namespace MyFridgeListWebapi.Application.Articles.Commands.Edit
{
    public sealed class EditArticleCommandValidator : AbstractValidator<EditArticleCommand>
    {
        public EditArticleCommandValidator(DatabaseContext dbContext)
        {
            RuleFor(request => request.ArticleId)
                .NotEmpty()
                .WithMessage(Resources.ValidationErrorMissingFridgeId);

            RuleFor(request => request.ArticleId)
                .Must(id => dbContext.Articles.Any(article => article.Id == id))
                .WithMessage(request => string.Format(Resources.ValidationErrorArticleWithIdNotExists, request.ArticleId));
        }
    }
}
