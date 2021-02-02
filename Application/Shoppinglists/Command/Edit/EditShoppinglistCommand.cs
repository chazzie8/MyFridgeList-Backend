using System;
using System.Text.Json.Serialization;
using MediatR;
using MyFridgeListWebapi.Core.Models.Responses.Shoppinglist;

namespace MyFridgeListWebapi.Application.Shoppinglists.Command.Edit
{
    public sealed class EditShoppinglistCommand : IRequest<EditShoppinglistResponse>
    {
        [JsonIgnore]
        public Guid UserId { get; set; }
        public Guid ShoppinglistId { get; set; }
        public string Name { get; set; }
    }
}
