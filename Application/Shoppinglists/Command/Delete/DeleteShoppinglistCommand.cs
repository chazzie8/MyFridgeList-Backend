using System;
using System.Text.Json.Serialization;
using MediatR;
using MyFridgeListWebapi.Core.Models.Responses.Shoppinglist;

namespace MyFridgeListWebapi.Application.Shoppinglists.Command.Delete
{
    public sealed class DeleteShoppinglistCommand : IRequest<DeleteShoppinglistResponse>
    {
        [JsonIgnore]
        public Guid UserId { get; set; }
        [JsonIgnore]
        public Guid ShoppinglistId { get; set; }
    }
}
