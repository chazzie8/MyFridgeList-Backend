using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MyFridgeListWebapi.Core.Data.Database;
using MyFridgeListWebapi.Core.Exceptions;
using MyFridgeListWebapi.Core.Models.Responses.Article;
using MyFridgeListWebapi.Properties;

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
            var fridge = await _databaseContext.Fridges
                .Where(x => x.UserId == request.UserId)
                .FirstOrDefaultAsync(x => x.Id == request.FridgeId);

            if (fridge == null)
            {
                throw new NotFoundException(string.Format(Resources.ValidationErrorFridgeWithIdNotExists, request.FridgeId));
            }

            var articles = await _databaseContext.Articles
                .Where(x => x.FridgeId == request.FridgeId)
                .Select(x => new ArticleResponse
                {
                    Id = x.Id,
                    Label = x.Label,
                    Amount = x.Amount,
                    Expirydate = x.ExpiryDate,
                    Timestamp = x.Timestamp,
                    Expirystatus = ExpiryCalculation.GetStatus(x.ExpiryDate),
                })
                .ToListAsync();

            return articles;
        }
    }
}
