using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using MediatR;
using MyFridgeListWebapi.Core.Models.Responses.Item;

namespace MyFridgeListWebapi.Application.Items.Command.Update
{
    public sealed class UpdateItemsCommand : IRequest<IEnumerable<ItemResponse>>
    {
        [JsonIgnore]
        public Guid UserId { get; set; }
        [JsonIgnore]
        public Guid ShoppinglistId { get; set; }
        public IList<Guid> ItemIds { get; set; }
    }
}
