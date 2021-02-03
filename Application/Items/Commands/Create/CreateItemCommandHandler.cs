using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using MyFridgeListWebapi.Core.Data.Database;
using MyFridgeListWebapi.Core.Data.Entities;
using MyFridgeListWebapi.Core.Models.Responses.Item;

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
            var item = new Item
            {
                Id = Guid.NewGuid(),
                Label = request.Label,
                Bought = false,
                ShoppinglistId = request.ShoppinglistId
            };

            _dbContext.Items.Add(item);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return new CreateItemResponse
            {
                Id = item.Id,
                Label = item.Label,
                Bought = item.Bought,
            };
        }
    }
}
