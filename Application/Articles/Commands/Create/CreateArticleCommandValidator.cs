using FluentValidation;
using MyFridgeListWebapi.Core.Data.Database;
using MyFridgeListWebapi.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyFridgeListWebapi.Application.Articles.Commands.Create
{
    public class CreateArticleCommandValidator : AbstractValidator<CreateArticleCommand>
    {
        public CreateArticleCommandValidator(DatabaseContext dbContext)
        {
            RuleFor(request => request.FridgeId)
                .Must(id => dbContext.Fridges.Any(fridge => fridge.Id == id))
                .WithMessage(request => string.Format(Resources.ValidationErrorFridgeWithIdNotExists, request.FridgeId));

            RuleFor(request => request.Label)
                .NotEmpty()
                .WithMessage(Resources.ValidationErrorMissingArticleLabel);

            RuleFor(request => request.Amount)
                .NotEmpty()
                .WithMessage(Resources.ValidationErrorMissingArticleAmount);

            RuleFor(request => request.ExpiryDate)
                .NotEmpty()
                .WithMessage(Resources.ValidationErrorMissingArticleExpiryDate);
        }
    }
}
