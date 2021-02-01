using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MyFridgeListWebapi.Core.Data.Database;
using MyFridgeListWebapi.Core.Models.Responses.Shoppinglist;

namespace MyFridgeListWebapi.Application.Shoppinglists.Queries.All
{
    public sealed class GetShoppinglistsQueryHandler : IRequestHandler<GetShoppinglistsQuery, IEnumerable<ShoppinglistResponse>>
    {
        private readonly DatabaseContext _databaseContext;

        public GetShoppinglistsQueryHandler(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public async Task<IEnumerable<ShoppinglistResponse>> Handle(GetShoppinglistsQuery request, CancellationToken cancellationToken)
        {
            var shoppinglists = await _databaseContext.Shoppinglists
                .Where(x => x.UserId == request.UserId)
                .Select(x => new ShoppinglistResponse
                {
                    Id = x.Id,
                    Name = x.Name,
                })
                .ToListAsync(cancellationToken);

            return shoppinglists;
        }
    }
}
