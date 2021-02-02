using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyFridgeListWebapi.Core.Data.Entities
{
    public sealed class Article
    {
        public Guid Id { get; set; }
        public string Label { get; set; }
        public int Amount { get; set; }
        public DateTime ExpiryDate { get; set; }
        public DateTime Timestamp { get; set; }
        public Fridge Fridge { get; set; }
        public Guid FridgeId { get; set; }
    }
}
