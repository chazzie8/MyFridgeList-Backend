using FluentValidation;
using MyFridgeListWebapi.Properties;

namespace MyFridgeListWebapi.Application.Articles.Commands.Create
{
    public class CreateArticleCommandValidator : AbstractValidator<CreateArticleCommand>
    {
        public CreateArticleCommandValidator()
        {
            RuleFor(request => request.FridgeId)
                .NotEmpty()
                .WithMessage(Resources.ValidationErrorMissingFridgeId);

            RuleFor(request => request.Label)
                .NotEmpty()
                .WithMessage(Resources.ValidationErrorMissingArticleLabel);

            RuleFor(request => request.Amount)
                .NotEmpty()
                .WithMessage(Resources.ValidationErrorMissingArticleAmount);

            RuleFor(request => request.Expirydate)
                .NotEmpty()
                .WithMessage(Resources.ValidationErrorMissingArticleExpiryDate);
        }
    }
}
