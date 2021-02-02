using MediatR;
using MyFridgeListWebapi.Core.Models.Responses.Article;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyFridgeListWebapi.Application.Articles.Queries.All
{
    public sealed class GetArticlesQuery : IRequest<IEnumerable<ArticleResponse>>
    {
        public Guid FridgeId { get; set; }
    }
}
