using System;
using System.Text.Json.Serialization;
using MediatR;
using MyFridgeListWebapi.Core.Models.Responses.Account;

namespace MyFridgeListWebapi.Application.Account.Commands.Delete
{
    public sealed class DeleteUserCommand : IRequest<DeleteUserResponse>
    {
        [JsonIgnore]
        public Guid UserId { get; set; }
    }
}
