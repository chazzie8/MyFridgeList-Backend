using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyFridgeListWebapi.Core.Models.Responses.Fridge
{
    public class CreateFridgeResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }
}
