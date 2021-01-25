using System.Collections.Generic;

namespace MyFridgeListWebapi.Core.Models
{
    public sealed class Response<T>
    {
        public Error Error { get; set; }
        public bool Success { get; set; }
        public T Data { get; set; }
        public IEnumerable<ValidationError> ValidationErrors { get; set; }
    }
}
