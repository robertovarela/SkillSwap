namespace RDS.Server;

public static class ConfigurationServer
{
    public static string ConnectionString { get; set; } = string.Empty;

    public const string CorsPolicyName = "wasm";
    public static string BackendUrl { get; set; } = string.Empty;
    public static string FrontendUrl { get; set; } = string.Empty;

    /*public static string JwtKey = "ZmVkYWY3ZDg4NjNiNDhlMTk3YjkyODdkNDkyYjcwOGU=";
    public static string JwtIssuer = "www.skillswap.mysoftwares.com.br";
    public static string JwtAudience = "GeneralAudience";
    public static readonly int JwtMinutesToRefresh = 4950;
    public static readonly int JwtMinutesToExpire = 5000;*/

    public static SmtpConfiguration Smtp = new();
    public static ResendConfiguration SmtpResend = new();

    public const int PageSize = 8;
    public const int MaxPagesToShow = 5;

    public const int DebounceDelay = 300; // milliseconds

    public class SmtpConfiguration
    {
        public string Host { get; set; } = null!;
        public int Port { get; set; } = 25;
        public string UserName { get; set; } = null!;
        public string Password { get; set; } = null!;
    }

    public class ResendConfiguration
    {
        public string Host { get; set; } = null!;
        public int Port { get; set; } = 587;
        public string UserName { get; set; } = null!;
        public string ResendApiKey { get; set; } = null!;
        public string FromEmail { get; set; } = null!;
    }

    public const int MinimumAge = 16;

    public static string? ClassMt = "mt-3";
    public static string? ClassMb = "mb-1";
}