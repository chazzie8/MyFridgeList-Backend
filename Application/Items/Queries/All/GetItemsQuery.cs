using MediatR;
using MyFridgeListWebapi.Core.Models.Responses.Item;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyFridgeListWebapi.Application.Items.Queries.All
{
    public sealed class GetItemsQuery : IRequest<IEnumerable<ItemResponse>>
    {
        public Guid ShoppinglistId { get; set; }
    }
}
