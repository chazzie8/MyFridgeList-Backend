﻿using System.Linq;
using FluentValidation;
using MyFridgeListWebapi.Core.Data.Database;
using MyFridgeListWebapi.Properties;

namespace MyFridgeListWebapi.Application.Fridges.Commands.Edit
{
    public sealed class EditFridgeCommandValidator : AbstractValidator<EditFridgeCommand>
    {
        public EditFridgeCommandValidator(DatabaseContext dbContext)
        {
            RuleFor(request => request.Id)
                .NotEmpty()
                .WithMessage(Resources.ValidationErrorMissingFridgeId);

            RuleFor(request => request.Id)
                .Must(id => dbContext.Fridges.Any(fridge => fridge.Id == id))
                .WithMessage(request => string.Format(Resources.ValidationErrorFridgeWithIdNotExists, request.Id));
        }
    }
}
