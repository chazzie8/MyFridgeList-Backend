namespace MyFridgeListWebapi.Core.Environment
{
    public sealed class JwtConfiguration
    {
        public string SecretKey { get; set; }
        public int LifetimeInSeconds { get; set; }
        public string Issuer { get; set; }
        public bool ValidateLifetime { get; set; }
        public bool IncludeErrorDetails { get; set; }
    }
}
