using System.Collections.Generic;
using MediatR;
using MyFridgeListWebapi.Core.Models.Responses.Users;

namespace MyFridgeListWebapi.Application.Users.Queries.All
{
    public sealed class GetUsersQuery : IRequest<IEnumerable<UserDetails>>
    {
    }
}
