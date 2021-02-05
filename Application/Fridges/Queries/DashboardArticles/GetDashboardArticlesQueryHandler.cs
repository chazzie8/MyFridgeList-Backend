using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MyFridgeListWebapi.Core.Data.Database;
using MyFridgeListWebapi.Core.Models.Responses.DashboardArticle;

namespace MyFridgeListWebapi.Application.Fridges.Queries.DashboardArticles
{
    public sealed class GetDashboardArticlesQueryHandler : IRequestHandler<GetDashboardArticlesQuery, IEnumerable<DashboardArticleResponse>>
    {
        private readonly DatabaseContext _dbContext;

        public GetDashboardArticlesQueryHandler(DatabaseContext databaseContext)
        {
            _dbContext = databaseContext;
        }
        public async Task<IEnumerable<DashboardArticleResponse>> Handle(GetDashboardArticlesQuery request, CancellationToken cancellationToken)
        {
            var fridgeIds = await _dbContext.Fridges
                .Where(x => x.UserId == request.UserId)
                .Select(x => x.Id)
                .ToListAsync(cancellationToken);

            var articles = await _dbContext.Articles
                .Where(x => fridgeIds.Contains(x.FridgeId))
                .Select(x => new DashboardArticleResponse
                {
                    Id = x.Id,
                    FridgeId = x.FridgeId,
                    ArticleName = x.Label,
                    Amount = x.Amount,
                    ExpiryDate = x.ExpiryDate,
                })
                .ToListAsync(cancellationToken);

            return articles;
        }
    }
}
