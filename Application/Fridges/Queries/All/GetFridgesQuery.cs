using System;
using System.Collections.Generic;
using MediatR;
using MyFridgeListWebapi.Core.Models.Responses.Fridge;

namespace MyFridgeListWebapi.Application.Fridges.Queries.All
{
    public class GetFridgesQuery : IRequest<IEnumerable<FridgeResponse>>
    {
        public Guid UserId { get; set; }
    }
}
