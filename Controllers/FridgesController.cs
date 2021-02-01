using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyFridgeListWebapi.Application.Fridges.Commands.Create;
using MyFridgeListWebapi.Application.Fridges.Commands.Delete;
using MyFridgeListWebapi.Application.Fridges.Commands.Edit;
using MyFridgeListWebapi.Application.Fridges.Queries.All;
using MyFridgeListWebapi.Core.Models;
using MyFridgeListWebapi.Core.Models.Responses.Fridge;

namespace MyFridgeListWebapi.Controllers
{
    [Authorize]
    [Microsoft.AspNetCore.Mvc.Route("api/[controller]")]
    public sealed class FridgesController : BaseController<FridgesController>
    {
        public FridgesController(IHttpContextAccessor contextAccessor)
            : base(contextAccessor)
        {
        }

        [HttpGet]
        public async Task<Response<IEnumerable<FridgeResponse>>> GetFridgesAsync()
        {
            var query = new GetFridgesQuery
            {
                UserId = UserId
            };

            return Success(await Mediator.Send(query));
        }

        [HttpPost]
        public async Task<Response<CreateFridgeResponse>> CreateFridgeAsync([FromBody] CreateFridgeCommand command)
        {
            command.UserId = UserId;

            return Success(await Mediator.Send(command));
        }

        [HttpDelete("{fridgeId}")]
        public async Task<ActionResult> DeleteFridgeAsync(Guid fridgeId, [FromBody] DeleteFridgeCommand command)
        {
            command.UserId = UserId;
            command.FridgeId = fridgeId;

            await Mediator.Send(command);
            return NoContent();
        }

        [HttpPut("{fridgeId}")]
        public async Task<Response<EditFridgeResponse>> EditFridgeAsync(Guid fridgeId, [FromBody] EditFridgeCommand command)
        {
            command.UserId = UserId;
            command.Id = fridgeId;

            return Success(await Mediator.Send(command));
        }
    }
}
