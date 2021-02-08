using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyFridgeListWebapi.Application.Account.Commands.SignIn;
using MyFridgeListWebapi.Application.Account.Commands.SignUp;
using MyFridgeListWebapi.Core.Exceptions;
using MyFridgeListWebapi.Core.Models;
using MyFridgeListWebapi.Core.Models.Responses;
using MyFridgeListWebapi.Core.Models.Responses.Account;

namespace MyFridgeListWebapi.Controllers
{
    [Microsoft.AspNetCore.Mvc.Route("api/[controller]")]
    public sealed class AccountController : BaseController<AccountController>
    {
        public AccountController(IHttpContextAccessor contextAccessor)
            : base(contextAccessor)
        {
        }

        [AllowAnonymous]
        [HttpPost("signup")]
        public async Task<Response<SignUpResponse>> SignUp([FromBody] SignUpCommand command)
        {
            return Success(await Mediator.Send(command));
        }

        [AllowAnonymous]
        [HttpPost("signin")]
        public async Task<Response<SignInResponse>> SignIn([FromBody] SignInCommand command)
        {
            return Success(await Mediator.Send(command));
        }
    }
}
