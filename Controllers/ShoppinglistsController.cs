using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyFridgeListWebapi.Application.Shoppinglists.Command.Create;
using MyFridgeListWebapi.Application.Shoppinglists.Command.Delete;
using MyFridgeListWebapi.Application.Shoppinglists.Command.Edit;
using MyFridgeListWebapi.Application.Shoppinglists.Queries.All;
using MyFridgeListWebapi.Core.Models;
using MyFridgeListWebapi.Core.Models.Responses.Shoppinglist;

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

        [HttpGet]
        public async Task<Response<IEnumerable<ShoppinglistResponse>>> GetFridgesAsync()
        {
            var query = new GetShoppinglistsQuery
            {
                UserId = UserId
            };

            return Success(await Mediator.Send(query));
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

        [HttpPut("{shoppinglistId}")]
        public async Task<Response<EditShoppinglistResponse>> EditShoppinglistAsync(Guid shoppinglistId, [FromBody] EditShoppinglistCommand command)
        {
            command.UserId = UserId;
            command.Id = shoppinglistId;

            return Success(await Mediator.Send(command));
        }
    }
}
