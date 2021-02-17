using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Identity;
using MyFridgeListWebapi.Core.Data.Database;
using MyFridgeListWebapi.Core.Data.Entities;
using MyFridgeListWebapi.Core.Exceptions;
using MyFridgeListWebapi.Core.Models.Responses.Account;
using MyFridgeListWebapi.Extensions;
using MyFridgeListWebapi.Properties;

namespace MyFridgeListWebapi.Application.Account.Commands.SignUp
{
    public sealed class SignUpCommandHandler : IRequestHandler<SignUpCommand, SignUpResponse>
    {
        private readonly UserManager<User> _userManager;
        private readonly DatabaseContext _dbContext;

        public SignUpCommandHandler(UserManager<User> userManager, DatabaseContext databaseContext)
        {
            _userManager = userManager;
            _dbContext = databaseContext;
        }

        public async Task<SignUpResponse> Handle(SignUpCommand request, CancellationToken cancellationToken)
        {
            var email = Uri.UnescapeDataString(request.Email);
            var emailExists = _dbContext.Users
                .Where(x => x.Email == request.Email)
                .Any();

            if (emailExists)
            {
                throw new BadRequestException(string.Format(Resources.ExceptionErrorEmailAddressAlreadyExists, request.Email));
            }

            var username = Uri.UnescapeDataString(request.Username);
            var usernameExists = _dbContext.Users
                .Where(x => x.UserName == request.Username)
                .Any();

            if (usernameExists)
            {
                throw new BadRequestException(string.Format(Resources.ExceptionErrorUsernameAlreadyExists, request.Username));
            }

            var password = Uri.UnescapeDataString(request.Password);
            var user = new User
            {
                Id = Guid.NewGuid(),
                UserName = username,
                Email = email,
                CreatedAt = DateTime.Now,
                ModifiedAt = DateTime.Now
            };

            var result = await _userManager.CreateAsync(user, password);

            return new SignUpResponse(result.Succeeded, result.Errors.ToValidationResults());
        }
    }
}
