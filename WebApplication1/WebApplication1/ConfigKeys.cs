namespace WebApplication1
{
    public class ConfigKeys
    {
        public class AzureAd
        {
            public const string Instance = "AzureAd:Instance";
            public const string TenantID = "AzureAd:TenantId";
            public const string ClientID = "AzureAd:ClientId";
            public const string ClientSecret = "AzureAd:ClientSecret";
            public const string CallbackPath = "AzureAd:CallbackPath";
        }

        public class Application
        {
            public const string EnvironmentName = "Application:EnvironmentName";
            public const string EnvironmentBaseUrl = "Application:EnvironmentBaseUrl";
            public const string TokenLifetimeMins = "Application:TokenLifetimeMins";
            public const string AttachDebuggerOnStart = "Application:AttachDebuggerOnStart";
        }
    }
}
