using MediatR;
using MyFridgeListWebapi.Core.Models.Responses.Fridge;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyFridgeListWebapi.Application.Fridges.Queries.All
{
    public class GetFridgesQuery : IRequest<IEnumerable<FridgeResponse>>
    {
        public Guid UserId { get; set; }
    }
}
