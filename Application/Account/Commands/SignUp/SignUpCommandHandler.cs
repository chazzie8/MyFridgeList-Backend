using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Identity;
using MyFridgeListWebapi.Core.Data.Entities;
using MyFridgeListWebapi.Core.Models.Responses;
using MyFridgeListWebapi.Core.Models.Responses.Account;
using MyFridgeListWebapi.Extensions;

namespace MyFridgeListWebapi.Application.Account.Commands.SignUp
{
    public sealed class SignUpCommandHandler : IRequestHandler<SignUpCommand, SignUpResponse>
    {
        private readonly UserManager<User> _userManager;

        public SignUpCommandHandler(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        public async Task<SignUpResponse> Handle(SignUpCommand request, CancellationToken cancellationToken)
        {
            var email = Uri.UnescapeDataString(request.Email);
            var password = Uri.UnescapeDataString(request.Password);
            var user = new User
            {
                Id = Guid.NewGuid(),
                UserName = email,
                Email = email,
                CreatedAt = DateTime.Now,
                ModifiedAt = DateTime.Now
            };

            var result = await _userManager.CreateAsync(user as User, password);

            return new SignUpResponse(result.Succeeded, result.Errors.ToValidationResults());
        }
    }
}
