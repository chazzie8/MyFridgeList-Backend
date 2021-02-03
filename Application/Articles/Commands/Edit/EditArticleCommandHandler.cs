using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MyFridgeListWebapi.Core.Data.Database;
using MyFridgeListWebapi.Core.Models.Responses.Article;

namespace MyFridgeListWebapi.Application.Articles.Commands.Edit
{
    public sealed class EditArticleCommandHandler : IRequestHandler<EditArticleCommand, EditArticleResponse>
    {
        private readonly DatabaseContext _dbContext;

        public EditArticleCommandHandler(DatabaseContext databaseContext)
        {
            _dbContext = databaseContext;
        }

        public async Task<EditArticleResponse> Handle(EditArticleCommand request, CancellationToken cancellationToken)
        {
            var article = await _dbContext.Articles
                .Where(x => x.FridgeId == request.FridgeId)
                .FirstOrDefaultAsync(x => x.Id == request.ArticleId, cancellationToken: cancellationToken);

            article.Label = request.Label;
            article.Amount = request.Amount;
            article.ExpiryDate = request.ExpiryDate;

            await _dbContext.SaveChangesAsync(cancellationToken);

            return new EditArticleResponse
            {
                Id = article.Id,
                Label = article.Label,
                Amount = article.Amount,
                ExpiryDate = article.ExpiryDate,
                Timestamp = article.Timestamp
            };
        }
    }
}
