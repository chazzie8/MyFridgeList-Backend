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
            RuleFor(x => x.Email)
                .NotEmpty()
                .WithMessage(Resources.ValidationErrorMissingEmailAddress);

            RuleFor(x => x.Email)
                .Must(email =>
                {
                    try
                    {
                        var mailAddress = new MailAddress(Uri.UnescapeDataString(email));

                        return true;
                    }
                    catch (Exception)
                    {
                        return false;
                    }
                })
                .WithMessage(Resources.ValidationErrorInvalidEmailAddress);

            RuleFor(request => request)
                .Must(request => databaseContext.Users.Any(user => user.Email == request.Email))
                .OnAnyFailure(request =>
                {
                    throw new UnauthorizedException(string.Format(Resources.ValidationErrorUserNotFoundByEmail, request.Email));
                });

            RuleFor(x => x.Password)
                .NotEmpty()
                .WithMessage(Resources.ValidationErrorMissingPassword);

            RuleFor(request => request.Password)
                .MinimumLength(appConfiguration.AccountConfiguration.PasswordRequiredLength)
                .WithMessage(Resources.ValidationErrorPasswordLessEightCharacter);
        }
    }
}