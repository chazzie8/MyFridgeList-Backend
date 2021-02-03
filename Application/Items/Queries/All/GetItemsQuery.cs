using System;
using System.Collections.Generic;
using MediatR;
using MyFridgeListWebapi.Core.Models.Responses.Item;

namespace MyFridgeListWebapi.Application.Items.Queries.All
{
    public sealed class GetItemsQuery : IRequest<IEnumerable<ItemResponse>>
    {
        public Guid ShoppinglistId { get; set; }
    }
}
