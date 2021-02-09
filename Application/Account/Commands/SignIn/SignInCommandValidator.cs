using System;
using System.Linq;
using System.Net.Mail;
using FluentValidation;
using MyFridgeListWebapi.Core.Data.Database;
using MyFridgeListWebapi.Core.Environment;
using MyFridgeListWebapi.Core.Exceptions;
using MyFridgeListWebapi.Properties;

namespace MyFridgeListWebapi.Application.Account.Commands.SignIn
{
    public sealed class SignInCommandValidator : AbstractValidator<SignInCommand>
    {
        public SignInCommandValidator(
            AppConfiguration appConfiguration,
            DatabaseContext databaseContext)
        {
            RuleFor(x => x.Password)
                .NotEmpty()
                .WithMessage(Resources.ValidationErrorMissingPassword);

            RuleFor(request => request.Password)
                .MinimumLength(appConfiguration.AccountConfiguration.PasswordRequiredLength)
                .WithMessage(Resources.ValidationErrorPasswordLessEightCharacter);
        }
    }
}