using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using MediatR;
using MyFridgeListWebapi.Core.Models.Responses.Article;

namespace MyFridgeListWebapi.Application.Articles.Queries.All
{
    public sealed class GetArticlesQuery : IRequest<IEnumerable<ArticleResponse>>
    {
        [JsonIgnore]
        public Guid UserId { get; set; }
        public Guid FridgeId { get; set; }
    }
}
