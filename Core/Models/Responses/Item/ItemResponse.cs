using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyFridgeListWebapi.Core.Models.Responses.Item
{
    public sealed class ItemResponse
    {
        public Guid Id { get; set; }
        public string Label { get; set; }
        public bool Bought { get; set; }
    }
}
