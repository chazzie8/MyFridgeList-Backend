using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyFridgeListWebapi.Core.Data.Entities
{
    public sealed class Item
    {
        public Guid Id { get; set; }
        public string Label { get; set; }
        public bool Bought { get; set; }
        public Guid ShoppinglistId { get; set; }
        public Shoppinglist Shoppinglist { get; set; }
    }
}
