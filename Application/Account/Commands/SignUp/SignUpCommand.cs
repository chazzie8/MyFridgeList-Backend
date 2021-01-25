using MediatR;
using MyFridgeListWebapi.Core.Models.Responses.Account;

namespace MyFridgeListWebapi.Application.Account.Commands.SignUp
{
    public sealed class SignUpCommand : IRequest<SignUpResponse>
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
