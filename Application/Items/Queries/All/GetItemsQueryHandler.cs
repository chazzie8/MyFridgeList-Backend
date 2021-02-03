using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MyFridgeListWebapi.Core.Data.Database;
using MyFridgeListWebapi.Core.Models.Responses.Item;

namespace MyFridgeListWebapi.Application.Items.Queries.All
{
    public sealed class GetItemsQueryHandler : IRequestHandler<GetItemsQuery, IEnumerable<ItemResponse>>
    {
        private readonly DatabaseContext _databaseContext;

        public GetItemsQueryHandler(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }
        public async Task<IEnumerable<ItemResponse>> Handle(GetItemsQuery request, CancellationToken cancellationToken)
        {
            var items = await _databaseContext.Items
                .Where(x => x.ShoppinglistId == request.ShoppinglistId)
                .Select(x => new ItemResponse
                {
                    Id = x.Id,
                    Label = x.Label,
                    Bought = x.Bought
                })
                .ToListAsync(cancellationToken);

            return items;
        }
    }
}
