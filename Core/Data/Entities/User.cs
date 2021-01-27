using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace MyFridgeListWebapi.Core.Data.Entities
{
    [Table("AspNetUsers")]
    public sealed class User : IdentityUser<Guid>
    {
        public DateTime CreatedAt { get; set; }
        public DateTime ModifiedAt { get; set; }
        public DateTime? FirstLoginAt { get; set; }
        public DateTime LastLoginAt { get; set; }
        public IList<Fridge> Fridges { get; set; }
        public IList<Shoppinglist> Shoppinglists { get; set; }
    }
}
