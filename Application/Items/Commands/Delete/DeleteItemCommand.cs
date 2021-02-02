using System;
using System.Text.Json.Serialization;
using MediatR;
using MyFridgeListWebapi.Core.Models.Responses.Item;

namespace MyFridgeListWebapi.Application.Items.Commands.Delete
{
    public sealed class DeleteItemCommand : IRequest<DeleteItemResponse>
    {
        [JsonIgnore]
        public Guid ShoppinglistId { get; set; }
        [JsonIgnore]
        public Guid ItemId { get; set; }
    }
}
