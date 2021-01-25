namespace MyFridgeListWebapi.Core.Environment
{
    public sealed class DatabaseConfiguration
    {
        public int DbContextPoolSize { get; set; }
        public int CommandTimeout { get; set; }
    }
}
