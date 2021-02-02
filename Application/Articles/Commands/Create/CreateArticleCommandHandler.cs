using MediatR;
using MyFridgeListWebapi.Core.Data.Database;
using MyFridgeListWebapi.Core.Data.Entities;
using MyFridgeListWebapi.Core.Models.Responses.Article;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MyFridgeListWebapi.Application.Articles.Commands.Create
{
    public sealed class CreateArticleCommandHandler : IRequestHandler<CreateArticleCommand, CreateArticleResponse>
    {
        private readonly DatabaseContext _dbContext;

        public CreateArticleCommandHandler(DatabaseContext databaseContext)
        {
            _dbContext = databaseContext;
        }
        public async Task<CreateArticleResponse> Handle(CreateArticleCommand request, CancellationToken cancellationToken)
        {
            var article = new Article
            {
                Id = Guid.NewGuid(),
                FridgeId = request.FridgeId,
                Label = request.Label,
                Amount = request.Amount,
                ExpiryDate = request.ExpiryDate,
                Timestamp = DateTime.Now
            };

            _dbContext.Articles.Add(article);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return new CreateArticleResponse
            {
                Id = article.Id,
                Label = article.Label,
                Amount = article.Amount,
                ExpiryDate = article.ExpiryDate,
                Timestamp = article.Timestamp,
            };
        }
    }
}
