using MediatR;
using Microsoft.EntityFrameworkCore;
using MyFridgeListWebapi.Core.Data.Database;
using MyFridgeListWebapi.Core.Models.Responses.Article;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MyFridgeListWebapi.Application.Articles.Queries.All
{
    public sealed class GetArticlesQueryHandler : IRequestHandler<GetArticlesQuery, IEnumerable<ArticleResponse>>
    {
        private readonly DatabaseContext _databaseContext;

        public GetArticlesQueryHandler(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }
        public async Task<IEnumerable<ArticleResponse>> Handle(GetArticlesQuery request, CancellationToken cancellationToken)
        {
            var articles = await _databaseContext.Articles
                .Where(x => x.FridgeId == request.FridgeId)
                .Select(x => new ArticleResponse
                {
                    Id = x.Id,
                    Label = x.Label,
                    Amount = x.Amount,
                    ExpiryDate = x.ExpiryDate,
                    Timestamp = x.Timestamp,
                })
                .ToListAsync(cancellationToken);

            return articles;
        }
    }
}
