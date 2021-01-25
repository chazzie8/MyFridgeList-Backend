using System;
using System.Collections.Generic;
using MyFridgeListWebapi.Core.Models;

namespace MyFridgeListWebapi.Core.Exceptions
{
    public sealed class UnauthorizedException : Exception
    {
        public UnauthorizedException(string message)
            : base(message)
        {
        }

        public IEnumerable<ValidationError> ValidationErrors { get; set; }
    }
}
