using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyFridgeListWebapi.Application.Users.Queries;
using MyFridgeListWebapi.Application.Users.Queries.All;
using MyFridgeListWebapi.Core.Models;
using MyFridgeListWebapi.Core.Models.Responses.Users;

namespace MyFridgeListWebapi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    public sealed class UsersController : BaseController<AccountController>
    {
        public UsersController(IHttpContextAccessor contextAccessor)
           : base(contextAccessor)
        {
        }

        [HttpGet]
        public async Task<Response<IEnumerable<UserDetails>>> GetUsersAsync()
        {
            var query = new GetUsersQuery();

            return Success(await Mediator.Send(query));
        }
    }
}
