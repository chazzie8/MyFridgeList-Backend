using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MyFridgeListWebapi.Core.Data.Database;
using MyFridgeListWebapi.Core.Models.Responses.Users;

namespace MyFridgeListWebapi.Application.Users.Queries.All
{
    public sealed class GetUsersQueryHandler : IRequestHandler<GetUsersQuery, IEnumerable<UserDetails>>
    {
        private readonly DatabaseContext _databaseContext;

        public GetUsersQueryHandler(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public async Task<IEnumerable<UserDetails>> Handle(GetUsersQuery request, CancellationToken cancellationToken)
        {
            var userDetails = await _databaseContext.Users
                .Select(user => new UserDetails
                {
                    Id = user.Id,
                    Email = user.Email,
                    FirstLoginAt = user.FirstLoginAt,
                })
                .ToListAsync();

            return userDetails;
        }
    }
}
