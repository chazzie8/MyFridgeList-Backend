namespace MyFridgeListWebapi.Core.Environment
{
    public sealed class AccountConfiguration
    {
        public bool RequireConfirmedEmail { get; set; }
        public bool RequireUniqueEmail { get; set; }
        public bool PasswordRequireDigit { get; set; }
        public bool PasswordRequireLowercase { get; set; }
        public bool PasswordRequireUppercase { get; set; }
        public bool PasswordRequireNonAlphanumeric { get; set; }
        public int PasswordRequiredLength { get; set; }
    }
}
