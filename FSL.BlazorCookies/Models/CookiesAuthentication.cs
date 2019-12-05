using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;

namespace FSL.BlazorCookies.Models
{
    public sealed class CookiesAuthentication
    {
        public ClaimsIdentity ClaimsIdentity { get; set; }
        public AuthenticationProperties AuthProperties { get; set; }
    }
}
