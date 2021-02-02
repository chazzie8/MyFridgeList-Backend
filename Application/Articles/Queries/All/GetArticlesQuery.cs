using System;
using System.Collections.Generic;
using MediatR;
using MyFridgeListWebapi.Core.Models.Responses.Article;

namespace MyFridgeListWebapi.Application.Articles.Queries.All
{
    public sealed class GetArticlesQuery : IRequest<IEnumerable<ArticleResponse>>
    {
        public Guid FridgeId { get; set; }
    }
}
