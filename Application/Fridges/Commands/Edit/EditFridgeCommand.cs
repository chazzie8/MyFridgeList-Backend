using System;
using System.Text.Json.Serialization;
using MediatR;
using MyFridgeListWebapi.Core.Models.Responses.Fridge;

namespace MyFridgeListWebapi.Application.Fridges.Commands.Edit
{
    public sealed class EditFridgeCommand : IRequest<EditFridgeResponse>
    {
        [JsonIgnore]
        public Guid UserId { get; set; }
        [JsonIgnore]
        public Guid FridgeId { get; set; }
        public string Name { get; set; }
    }
}
