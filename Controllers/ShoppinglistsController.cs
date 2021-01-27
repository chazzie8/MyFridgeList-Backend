using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyFridgeListWebapi.Application.Shoppinglists.Command.Create;
using MyFridgeListWebapi.Application.Shoppinglists.Command.Delete;
using MyFridgeListWebapi.Core.Models;
using MyFridgeListWebapi.Core.Models.Responses.Shoppinglist;
using System;
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

        [HttpDelete("{shoppinglistId}")]
        public async Task<ActionResult> DeleteShoppinglistAsync(Guid shoppinglistId, [FromBody] DeleteShoppinglistCommand command)
        {
            command.UserId = UserId;
            command.ShoppinglistId = shoppinglistId;

            await Mediator.Send(command);
            return NoContent();
        }
    }
}
