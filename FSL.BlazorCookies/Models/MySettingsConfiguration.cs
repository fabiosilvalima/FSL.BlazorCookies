namespace FSL.BlazorCookies.Models
{
    public sealed class MySettingsConfiguration
    {
        public int ExpirationInSeconds { get; set; }
        public string CryptographicKey { get; set; }
        public string CookieName { get; set; }
    }
}
