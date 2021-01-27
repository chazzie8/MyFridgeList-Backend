using System;
using System.Text.Json.Serialization;
using MediatR;
using MyFridgeListWebapi.Core.Models.Responses.Shoppinglist;

namespace MyFridgeListWebapi.Application.Shoppinglists.Command.Create
{
    public sealed class CreateShoppinglistCommand : IRequest<CreateShoppinglistResponse>
    {
        [JsonIgnore]
        public Guid UserId { get; set; }
        public string Name { get; set; }
    }
}
