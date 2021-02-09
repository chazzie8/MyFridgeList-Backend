using System;
using System.Net.Mail;
using FluentValidation;
using MyFridgeListWebapi.Core.Data.Database;
using MyFridgeListWebapi.Core.Environment;
using MyFridgeListWebapi.Properties;

namespace MyFridgeListWebapi.Application.Account.Commands.SignUp
{
    public sealed class SignUpCommandValidator : AbstractValidator<SignUpCommand>
    {
        public SignUpCommandValidator(AppConfiguration appConfiguration, DatabaseContext dbContext)
        {
            RuleFor(request => request.Username)
                .NotEmpty()
                .WithMessage(Resources.ValidationErrorMissingUsername);

            RuleFor(request => request.Email)
                .NotEmpty()
                .WithMessage(Resources.ValidationErrorMissingEmailAddress);

            RuleFor(request => request.Email)
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

            RuleFor(request => request.Password)
                .NotEmpty()
                .WithMessage(Resources.ValidationErrorMissingPassword);

            RuleFor(request => request.Password)
                .MinimumLength(appConfiguration.AccountConfiguration.PasswordRequiredLength)
                .WithMessage(Resources.ValidationErrorPasswordLessEightCharacter);
        }
    }
}
