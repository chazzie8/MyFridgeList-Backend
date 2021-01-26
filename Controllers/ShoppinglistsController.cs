using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyFridgeListWebapi.Application.Shoppinglists.Command.Create;
using MyFridgeListWebapi.Core.Models;
using MyFridgeListWebapi.Core.Models.Responses.Shoppinglist;
using System.Threading.Tasks;

namespace MyFridgeListWebapi.Controllers
{
    [Authorize]
    [Microsoft.AspNetCore.Mvc.Route("api/[controller]")]
    public sealed class ShoppinglistsController : BaseController<ShoppinglistsController>
    {
        public ShoppinglistsController(IHttpContextAccessor contextAccessor)
            : base(contextAccessor)
        {
        }

        [HttpPost]
        public async Task<Response<CreateShoppinglistResponse>> CreateShoppinglistAsync([FromBody] CreateShoppinglistCommand command)
        {
            command.UserId = UserId;

            return Success(await Mediator.Send(command));
        }
    }
}
