using System;

namespace MyFridgeListWebapi.Core.Models.Responses.Account
{
    public sealed class SignInResponse
    {
        public SignInResponse(bool succeeded)
        {
            Succeeded = succeeded;
        }

        public string Token { get; set; }
        public Guid UserId { get; set; }
        public DateTime ValidFrom { get; set; }
        public DateTime ExpiredAt { get; set; }
        public bool Succeeded { get; }
    }
}
