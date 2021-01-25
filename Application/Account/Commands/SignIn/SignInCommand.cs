using MediatR;
using MyFridgeListWebapi.Core.Models.Responses.Account;

namespace MyFridgeListWebapi.Application.Account.Commands.SignIn
{
    public sealed class SignInCommand : IRequest<SignInResponse>
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
