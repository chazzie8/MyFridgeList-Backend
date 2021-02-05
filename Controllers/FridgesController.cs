using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyFridgeListWebapi.Application.Articles.Commands.Create;
using MyFridgeListWebapi.Application.Articles.Commands.Delete;
using MyFridgeListWebapi.Application.Articles.Commands.Edit;
using MyFridgeListWebapi.Application.Articles.Queries.All;
using MyFridgeListWebapi.Application.Fridges.Commands.Create;
using MyFridgeListWebapi.Application.Fridges.Commands.Delete;
using MyFridgeListWebapi.Application.Fridges.Commands.Edit;
using MyFridgeListWebapi.Application.Fridges.Queries.All;
using MyFridgeListWebapi.Application.Fridges.Queries.DashboardArticles;
using MyFridgeListWebapi.Core.Models;
using MyFridgeListWebapi.Core.Models.Responses.Article;
using MyFridgeListWebapi.Core.Models.Responses.DashboardArticle;
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

        [HttpGet("dashboard-items")]
        public async Task<Response<IEnumerable<DashboardArticleResponse>>> GetDashboardArticlesAsync()
        {
            var query = new GetDashboardArticlesQuery
            {
                UserId = UserId
            };

            return Success(await Mediator.Send(query));
        }

        [HttpDelete("{fridgeId}")]
        public async Task<Response<DeleteFridgeResponse>> DeleteFridgeAsync([FromRoute] Guid fridgeId)
        {
            var command = new DeleteFridgeCommand
            {
                UserId = UserId,
                FridgeId = fridgeId
            };

            return Success(await Mediator.Send(command));
        }

        [HttpPut("{fridgeId}")]
        public async Task<Response<EditFridgeResponse>> EditFridgeAsync([FromRoute] Guid fridgeId, [FromBody] EditFridgeCommand command)
        {
            command.UserId = UserId;
            command.FridgeId = fridgeId;

            return Success(await Mediator.Send(command));
        }

        [HttpGet("{fridgeId}/articles")]
        public async Task<Response<IEnumerable<ArticleResponse>>> GetArticlesAsync([FromRoute] Guid fridgeId)
        {
            var query = new GetArticlesQuery
            {
                UserId = UserId,
                FridgeId = fridgeId
            };

            return Success(await Mediator.Send(query));
        }

        [HttpPost("{fridgeId}/articles")]
        public async Task<Response<CreateArticleResponse>> CreateArticleAsync([FromRoute] Guid fridgeId, [FromBody] CreateArticleCommand command)
        {
            command.UserId = UserId;
            command.FridgeId = fridgeId;

            return Success(await Mediator.Send(command));
        }

        [HttpPut("{fridgeId}/articles/{articleId}")]
        public async Task<Response<EditArticleResponse>> EditArticleAsync([FromRoute] Guid fridgeId, [FromRoute] Guid articleId, [FromBody] EditArticleCommand command)
        {
            command.UserId = UserId;
            command.FridgeId = fridgeId;
            command.ArticleId = articleId;

            return Success(await Mediator.Send(command));
        }

        [HttpDelete("{fridgeId}/articles/{articleId}")]
        public async Task<ActionResult> DeleteArticleAsync([FromRoute] Guid fridgeId, [FromRoute] Guid articleId)
        {
            var command = new DeleteArticleCommand
            {
                UserId = UserId,
                FridgeId = fridgeId,
                ArticleId = articleId
            };

            await Mediator.Send(command);
            return NoContent();
        }
    }
}
