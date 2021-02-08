using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Identity;
using MyFridgeListWebapi.Core.Data.Database;
using MyFridgeListWebapi.Core.Data.Entities;
using MyFridgeListWebapi.Core.Exceptions;
using MyFridgeListWebapi.Core.Models.Responses.Account;
using MyFridgeListWebapi.Core.Services;
using MyFridgeListWebapi.Extensions;
using MyFridgeListWebapi.Properties;

namespace MyFridgeListWebapi.Application.Account.Commands.SignIn
{
    public sealed class SignInCommandHandler : IRequestHandler<SignInCommand, SignInResponse>
    {
        private readonly DatabaseContext _dbContext;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IJwTokenService _tokenService;

        public SignInCommandHandler(
            DatabaseContext dbContext,
            UserManager<User> userManager,
            SignInManager<User> signInManager,
            IJwTokenService tokenService)
        {
            _dbContext = dbContext;
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenService = tokenService;
        }

        public async Task<SignInResponse> Handle(SignInCommand request, CancellationToken cancellationToken)
        {
            var email = Uri.UnescapeDataString(request.Email);
            var password = Uri.UnescapeDataString(request.Password);
            var user = await _userManager.FindByEmailAsync(email);
            var result = await _signInManager.PasswordSignInAsync(user.UserName, password, false, false);

            if(result.Succeeded == false)
            {
                throw new UnauthorizedException(Resources.ExceptionErrorWrongEmailPassword);
            }

            await UpdateUserAsync(result, user);

            return CreateSignInResponse(result, user);
        }

        private async Task UpdateUserAsync(SignInResult result, User user)
        {
            if (result.Succeeded)
            {
                var loginDate = DateTime.Now;
                user.FirstLoginAt = user.FirstLoginAt == null ? loginDate : user.FirstLoginAt;
                user.LastLoginAt = loginDate;

                await _dbContext.SaveChangesAsync();
            }
        }

        private SignInResponse CreateSignInResponse(SignInResult result, User user)
        {
            if (result.Succeeded)
            {
                var token = _tokenService.CreateToken(user);

                return new SignInResponse(true)
                {
                    UserId = token.UserId(),
                    Username = user.UserName,
                    Token = token.Token(),
                    ExpiredAt = token.ValidTo
                };
            }

            return new SignInResponse(false);
        }
    }
}
