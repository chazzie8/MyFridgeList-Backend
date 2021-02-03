using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MyFridgeListWebapi.Core.Data.Database;
using MyFridgeListWebapi.Core.Models.Responses.Article;

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
            var article = await _dbContext.Articles
                .Where(x => x.Id == request.ArticleId)
                .FirstOrDefaultAsync();

            _dbContext.Articles.Remove(article);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return new DeleteArticleResponse();
        }
    }
}
