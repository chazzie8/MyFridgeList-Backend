using System;
using System.Collections.Generic;
using MediatR;
using MyFridgeListWebapi.Core.Models.Responses.Shoppinglist;

namespace MyFridgeListWebapi.Application.Shoppinglists.Queries.All
{
    public sealed class GetShoppinglistsQuery : IRequest<IEnumerable<ShoppinglistResponse>>
    {
        public Guid UserId { get; set; }
    }
}
