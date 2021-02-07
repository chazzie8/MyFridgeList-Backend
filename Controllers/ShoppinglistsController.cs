using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyFridgeListWebapi.Application.Items.Commands.Create;
using MyFridgeListWebapi.Application.Items.Commands.Delete;
using MyFridgeListWebapi.Application.Items.Queries.All;
using MyFridgeListWebapi.Application.Shoppinglists.Command.Create;
using MyFridgeListWebapi.Application.Shoppinglists.Command.Delete;
using MyFridgeListWebapi.Application.Shoppinglists.Command.Edit;
using MyFridgeListWebapi.Application.Shoppinglists.Command.Update;
using MyFridgeListWebapi.Application.Shoppinglists.Queries.All;
using MyFridgeListWebapi.Core.Models;
using MyFridgeListWebapi.Core.Models.Responses.Item;
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
        public async Task<Response<DeleteShoppinglistResponse>> DeleteShoppinglistAsync([FromRoute] Guid shoppinglistId)
        {
            var command = new DeleteShoppinglistCommand
            {
                UserId = UserId,
                ShoppinglistId = shoppinglistId,
            };

            return Success(await Mediator.Send(command));
        }

        [HttpPut("{shoppinglistId}")]
        public async Task<Response<EditShoppinglistResponse>> EditShoppinglistAsync([FromRoute] Guid shoppinglistId, [FromBody] EditShoppinglistCommand command)
        {
            command.UserId = UserId;
            command.ShoppinglistId = shoppinglistId;

            return Success(await Mediator.Send(command));
        }

        [HttpGet("{shoppinglistId}/items")]
        public async Task<Response<IEnumerable<ItemResponse>>> GetItemsAsync([FromRoute] Guid shoppinglistId)
        {
            var query = new GetItemsQuery
            {
                UserId = UserId,
                ShoppinglistId = shoppinglistId
            };

            return Success(await Mediator.Send(query));
        }

        [HttpPost("{shoppinglistId}/items")]
        public async Task<Response<CreateItemResponse>> CreateItemAsync([FromRoute] Guid shoppinglistId, [FromBody] CreateItemCommand command)
        {
            command.UserId = UserId;
            command.ShoppinglistId = shoppinglistId;

            return Success(await Mediator.Send(command));
        }

        [HttpDelete("{shoppinglistId}/items/{itemId}")]
        public async Task<ActionResult> CreateArticleAsync([FromRoute] Guid shoppinglistId, [FromRoute] Guid itemId)
        {
            var command = new DeleteItemCommand
            {
                UserId = UserId,
                ShoppinglistId = shoppinglistId,
                ItemId = itemId
            };

            await Mediator.Send(command);
            return NoContent();
        }

        [HttpPost("{shoppinglistId}/update")]
        public async Task<Response<IEnumerable<ItemResponse>>> UpdateItemsAsync([FromRoute] Guid shoppinglistId, [FromBody] UpdateItemsCommand command)
        {
            command.UserId = UserId;
            command.ShoppinglistId = shoppinglistId;

            return Success(await Mediator.Send(command));
        }
    }
}
