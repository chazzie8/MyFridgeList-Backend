using System;
using System.Collections.Generic;
using MyFridgeListWebapi.Core.Models;

namespace MyFridgeListWebapi.Core.Exceptions
{
    public sealed class ValidationException : Exception
    {
        public ValidationException(string message = "One or more validation failures have occurred.")
            : base(message)
        {
            ValidationErrors = new List<ValidationError>();
        }

        public ValidationException(string message, params ValidationError[] validationErrors)
            : this(message)
        {
            ValidationErrors = validationErrors;
        }

        public ValidationException(IEnumerable<ValidationError> validationErrors)
            : this()
        {
            ValidationErrors = validationErrors;
        }

        public IEnumerable<ValidationError> ValidationErrors { get; }
    }
}