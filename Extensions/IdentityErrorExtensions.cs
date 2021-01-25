using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Identity;
using MyFridgeListWebapi.Core.Models;

namespace MyFridgeListWebapi.Extensions
{
    public static class IdentityErrorExtensions
    {
        public static IEnumerable<ValidationError> ToValidationResults(this IEnumerable<IdentityError> errors)
        {
            return errors
                .Select(error => new ValidationError
                {
                    Field = error.Code,
                    Messages = new[] { error.Description }
                });
        }
    }
}