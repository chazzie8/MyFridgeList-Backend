using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MyFridgeListWebapi.Core.Data.Database;
using MyFridgeListWebapi.Core.Exceptions;
using MyFridgeListWebapi.Core.Models.Responses.Item;
using MyFridgeListWebapi.Properties;

namespace MyFridgeListWebapi.Application.Items.Queries.All
{
    public sealed class GetItemsQueryHandler : IRequestHandler<GetItemsQuery, IEnumerable<ItemResponse>>
    {
        private readonly DatabaseContext _dbContext;

        public GetItemsQueryHandler(DatabaseContext databaseContext)
        {
            _dbContext = databaseContext;
        }
        public async Task<IEnumerable<ItemResponse>> Handle(GetItemsQuery request, CancellationToken cancellationToken)
        {
            var shoppinglist = await _dbContext.Shoppinglists
                .Where(x => x.UserId == request.UserId)
                .FirstOrDefaultAsync(x => x.Id == request.ShoppinglistId);

            if (shoppinglist == null)
            {
                throw new NotFoundException(string.Format(Resources.ValidationErrorShoppinglistWithIdNotExists, request.ShoppinglistId));
            }

            var items = await _dbContext.Items
                .Where(x => x.ShoppinglistId == request.ShoppinglistId)
                .Select(x => new ItemResponse
                {
                    Id = x.Id,
                    Label = x.Label,
                    Bought = x.Bought
                })
                .ToListAsync();

            return items;
        }
    }
}
