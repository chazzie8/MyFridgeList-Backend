using FluentValidation;
using MyFridgeListWebapi.Properties;

namespace MyFridgeListWebapi.Application.Articles.Commands.Delete
{
    public sealed class DeleteArticleCommandValidator : AbstractValidator<DeleteArticleCommand>
    {
        public DeleteArticleCommandValidator()
        {
            RuleFor(request => request.FridgeId)
                .NotEmpty()
                .WithMessage(request => string.Format(Resources.ValidationErrorMissingFridgeId));

            RuleFor(request => request.ArticleId)
                .NotEmpty()
                .WithMessage(request => string.Format(Resources.ValidationErrorMissingArticleId));
        }
    }
}
