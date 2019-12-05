using FSL.BlazorCookies.Models;
using System.Threading.Tasks;

namespace FSL.BlazorCookies.Service
{
    public interface IAuthenticationService
    {
        Task<AuthenticationResult> AuthenticateAsync(
            IUser user);

        string GetAuthenticationSchema();
        
        Task LogoutAsync();
    }
}
