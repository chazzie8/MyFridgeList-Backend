using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MyFridgeListWebapi.Core.Data.Database;
using MyFridgeListWebapi.Core.Exceptions;
using MyFridgeListWebapi.Core.Models.Responses.Article;
using MyFridgeListWebapi.Properties;

namespace MyFridgeListWebapi.Application.Articles.Commands.Delete
{
    public sealed class DeleteArticleCommandHandler : IRequestHandler<DeleteArticleCommand, DeleteArticleResponse>
    {
        private readonly DatabaseContext _dbContext;

        public DeleteArticleCommandHandler(DatabaseContext databaseContext)
        {
            _dbContext = databaseContext;
        }

        public async Task<DeleteArticleResponse> Handle(DeleteArticleCommand request, CancellationToken cancellationToken)
        {
            var fridge = await _dbContext.Fridges
                .Where(x => x.UserId == request.UserId)
                .FirstOrDefaultAsync(x => x.Id == request.FridgeId);

            if (fridge == null)
            {
                throw new NotFoundException(string.Format(Resources.ValidationErrorFridgeWithIdNotExists, request.FridgeId));
            }

            var article = await _dbContext.Articles
                .Where(x => x.FridgeId == request.FridgeId)
                .FirstOrDefaultAsync(x => x.Id == request.ArticleId);

            if (article == null)
            {
                throw new NotFoundException(string.Format(Resources.ValidationErrorArticleWithIdNotExists, request.ArticleId));
            }

            _dbContext.Articles.Remove(article);
            await _dbContext.SaveChangesAsync();

            return new DeleteArticleResponse();
        }
    }
}
