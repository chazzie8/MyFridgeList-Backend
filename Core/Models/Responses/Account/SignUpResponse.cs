using System.Collections.Generic;

namespace MyFridgeListWebapi.Core.Models.Responses.Account
{
    public sealed class SignUpResponse
    {
        public SignUpResponse()
        {
        }

        public SignUpResponse(bool succeeded, IEnumerable<ValidationError> validationErrors)
        {
            Succeeded = succeeded;
            ValidationErrors = validationErrors;
        }

        public bool Succeeded { get; }
        public IEnumerable<ValidationError> ValidationErrors { get; }
    }
}
