using System;

namespace MyFridgeListWebapi.Core.Data.Entities
{
    public sealed class Fridge
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Guid UserId { get; set; }
        public User User { get; set; }
    }
}
