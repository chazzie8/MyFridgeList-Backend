using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MyFridgeListWebapi.Application.Articles.Queries.All;
using MyFridgeListWebapi.Core.Data.Database;
using MyFridgeListWebapi.Core.Data.Entities;
using MyFridgeListWebapi.Core.Exceptions;
using MyFridgeListWebapi.Core.Models.Responses.Article;
using MyFridgeListWebapi.Properties;

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
            var fridge = await _dbContext.Fridges
                .Where(x => x.UserId == request.UserId)
                .FirstOrDefaultAsync(x => x.Id == request.FridgeId);

            if (fridge == null)
            {
                throw new NotFoundException(string.Format(Resources.ValidationErrorFridgeWithIdNotExists, request.FridgeId));
            }

            var article = new Article
            {
                Id = Guid.NewGuid(),
                FridgeId = request.FridgeId,
                Label = request.Label,
                Amount = request.Amount,
                ExpiryDate = request.Expirydate,
                Timestamp = DateTime.Now,
            };

            _dbContext.Articles.Add(article);
            await _dbContext.SaveChangesAsync();

            return new CreateArticleResponse
            {
                Id = article.Id,
                Label = article.Label,
                Amount = article.Amount,
                Expirydate = article.ExpiryDate,
                Timestamp = article.Timestamp,
                Expirystatus = ExpiryCalculation.GetStatus(article.ExpiryDate),
            };
        }
    }
}
