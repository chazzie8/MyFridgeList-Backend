using System;
using System.Text.Json.Serialization;
using MediatR;
using MyFridgeListWebapi.Core.Models.Responses.Item;

namespace MyFridgeListWebapi.Application.Items.Commands.Create
{
    public class CreateItemCommand : IRequest<CreateItemResponse>
    {
        [JsonIgnore]
        public Guid ShoppinglistId { get; set; }
        public string Label { get; set; }
    }
}
