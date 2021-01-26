using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyFridgeListWebapi.Application.Fridges.Commands.Create;
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

        [HttpPost]
        public async Task<Response<CreateFridgeResponse>> CreateFridgeAsync([FromBody] CreateFridgeCommand command)
        {
            command.UserId = UserId;

            return Success(await Mediator.Send(command));
        }
    }
}
