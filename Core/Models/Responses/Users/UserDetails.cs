using System;

namespace MyFridgeListWebapi.Core.Models.Responses.Users
{
    public sealed class UserDetails
    {
        public string Email { get; set; }
        public DateTime? FirstLoginAt { get; set; }
        public Guid Id { get; set; }
    }
}
