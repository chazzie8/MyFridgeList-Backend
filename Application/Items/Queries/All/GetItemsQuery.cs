using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using MediatR;
using MyFridgeListWebapi.Core.Models.Responses.Item;

namespace MyFridgeListWebapi.Application.Items.Queries.All
{
    public sealed class GetItemsQuery : IRequest<IEnumerable<ItemResponse>>
    {
        [JsonIgnore]
        public Guid UserId { get; set; }
        public Guid ShoppinglistId { get; set; }
    }
}
