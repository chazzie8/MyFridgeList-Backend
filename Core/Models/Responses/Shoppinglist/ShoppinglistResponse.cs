﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyFridgeListWebapi.Core.Models.Responses.Shoppinglist
{
    public sealed class ShoppinglistResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }
}
