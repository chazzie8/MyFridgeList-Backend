namespace MyFridgeListWebapi.Core.Models
{
    public sealed class ValidationError
    {
        public ValidationError()
        {
        }

        public ValidationError(string code, string description)
        {
            Field = code;
            Messages = new[] { description };
        }

        public string Field { get; set; }
        public string[] Messages { get; set; }
    }
}
