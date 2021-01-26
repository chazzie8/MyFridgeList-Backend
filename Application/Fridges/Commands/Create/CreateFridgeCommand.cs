using System;
using System.Text.Json.Serialization;
using MediatR;
using MyFridgeListWebapi.Core.Models.Responses.Fridge;

namespace MyFridgeListWebapi.Application.Fridges.Commands.Create
{
    public sealed class CreateFridgeCommand : IRequest<CreateFridgeResponse>
    {
        [JsonIgnore]
        public Guid UserId { get; set; }
        public string Name { get; set; }
    }
}
