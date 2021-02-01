using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MyFridgeListWebapi.Core.Data.Database;
using MyFridgeListWebapi.Core.Models.Responses.Fridge;

namespace MyFridgeListWebapi.Application.Fridges.Queries.All
{
    public sealed class GetFridgesQueryHandler : IRequestHandler<GetFridgesQuery, IEnumerable<FridgeResponse>>
    {
        private readonly DatabaseContext _databaseContext;

        public GetFridgesQueryHandler(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public async Task<IEnumerable<FridgeResponse>> Handle(GetFridgesQuery request, CancellationToken cancellationToken)
        {
            var fridges = await _databaseContext.Fridges
                .Where(x => x.UserId == request.UserId)
                .Select(x => new FridgeResponse
                {
                    Id = x.Id,
                    Name = x.Name,
                })
                .ToListAsync(cancellationToken);

            return fridges;
        }
    }
}
