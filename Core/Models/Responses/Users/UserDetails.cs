using System;

namespace MyFridgeListWebapi.Core.Models.Responses.Users
{
    public sealed class UserDetails
    {
        public string Email { get; internal set; }
        public DateTime? FirstLoginAt { get; internal set; }
        public Guid Id { get; internal set; }
    }
}
