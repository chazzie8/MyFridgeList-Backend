using System;
using System.Text.Json.Serialization;
using MediatR;
using MyFridgeListWebapi.Core.Models.Responses.Fridge;

namespace MyFridgeListWebapi.Application.Fridges.Commands.Delete
{
    public sealed class DeleteFridgeCommand : IRequest<DeleteFridgeResponse>
    {
        [JsonIgnore]
        public Guid UserId { get; set; }
        [JsonIgnore]
        public Guid FridgeId { get; set; }
    }
}
