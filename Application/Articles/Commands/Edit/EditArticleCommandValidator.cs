using FluentValidation;
using MyFridgeListWebapi.Properties;

namespace MyFridgeListWebapi.Application.Articles.Commands.Edit
{
    public sealed class EditArticleCommandValidator : AbstractValidator<EditArticleCommand>
    {
        public EditArticleCommandValidator()
        {
            RuleFor(request => request.FridgeId)
                .NotEmpty()
                .WithMessage(Resources.ValidationErrorMissingFridgeId);

            RuleFor(request => request.ArticleId)
                .NotEmpty()
                .WithMessage(Resources.ValidationErrorMissingArticleId);
        }
    }
}
