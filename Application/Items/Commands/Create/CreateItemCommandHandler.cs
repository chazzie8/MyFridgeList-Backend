using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MyFridgeListWebapi.Core.Data.Database;
using MyFridgeListWebapi.Core.Data.Entities;
using MyFridgeListWebapi.Core.Exceptions;
using MyFridgeListWebapi.Core.Models.Responses.Item;
using MyFridgeListWebapi.Properties;

namespace MyFridgeListWebapi.Application.Items.Commands.Create
{
    public sealed class CreateItemCommandHandler : IRequestHandler<CreateItemCommand, CreateItemResponse>
    {
        private readonly DatabaseContext _dbContext;

        public CreateItemCommandHandler(DatabaseContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<CreateItemResponse> Handle(CreateItemCommand request, CancellationToken cancellationToken)
        {
            var shoppinglist = await _dbContext.Shoppinglists
                .Where(x => x.UserId == request.UserId)
                .FirstOrDefaultAsync(x => x.Id == request.ShoppinglistId);

            if (shoppinglist == null)
            {
                throw new NotFoundException(string.Format(Resources.ValidationErrorShoppinglistWithIdNotExists, request.ShoppinglistId));
            }

            var item = new Item
            {
                Id = Guid.NewGuid(),
                Label = request.Label,
                Bought = false,
                ShoppinglistId = request.ShoppinglistId
            };

            _dbContext.Items.Add(item);
            await _dbContext.SaveChangesAsync();

            return new CreateItemResponse
            {
                Id = item.Id,
                Label = item.Label,
                Bought = item.Bought,
            };
        }
    }
}
