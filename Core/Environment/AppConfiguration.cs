using System.Collections.Generic;

namespace MyFridgeListWebapi.Core.Environment
{
    public sealed class AppConfiguration
    {
        public const string DefaultConnectionIdentifier = "DefaultConnection";

        public IDictionary<string, string> ConnectionStrings { get; set; }

        public string DefaultConnectionString
        {
            get
            {
                return ConnectionStrings != null && ConnectionStrings.ContainsKey(DefaultConnectionIdentifier)
                    ? ConnectionStrings[DefaultConnectionIdentifier]
                    : string.Empty;
            }
        }

        public AccountConfiguration AccountConfiguration { get; set; }
        public JwtConfiguration JwtConfiguration { get; set; }
        public DatabaseConfiguration DatabaseConfiguration { get; set; }
    }
}
