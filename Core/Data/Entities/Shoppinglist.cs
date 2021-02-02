using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyFridgeListWebapi.Core.Data.Entities
{
    public sealed class Shoppinglist
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Guid UserId { get; set; }
        public User User { get; set; }
        public IList<Item> Items { get; set; }
    }
}
