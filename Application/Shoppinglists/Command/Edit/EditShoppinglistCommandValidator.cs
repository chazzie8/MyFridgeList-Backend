﻿using System.Linq;
using FluentValidation;
using MyFridgeListWebapi.Core.Data.Database;
using MyFridgeListWebapi.Properties;

namespace MyFridgeListWebapi.Application.Shoppinglists.Command.Edit
{
    public sealed class EditShoppinglistCommandValidator : AbstractValidator<EditShoppinglistCommand>
    {
        public EditShoppinglistCommandValidator(DatabaseContext dbContext)
        {
            RuleFor(request => request.ShoppinglistId)
                .NotEmpty()
                .WithMessage(Resources.ValidationErrorMissingShoppinglistId);

            RuleFor(request => request.ShoppinglistId)
                .Must(id => dbContext.Shoppinglists.Any(shoppinglist => shoppinglist.Id == id))
                .WithMessage(request => string.Format(Resources.ValidationErrorShoppinglistWithIdNotExists, request.ShoppinglistId));
        }
    }
}
