using System;
using System.Collections.Generic;
using MyFridgeListWebapi.Core.Models;

namespace MyFridgeListWebapi.Core.Exceptions
{
    public sealed class NotFoundException : Exception
    {
        public NotFoundException(string message)
            : base(message)
        {
            ValidationErrors = new List<ValidationError>();
        }

        public IEnumerable<ValidationError> ValidationErrors { get; set; }
    }
}
