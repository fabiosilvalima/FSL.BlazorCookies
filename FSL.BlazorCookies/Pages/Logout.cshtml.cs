using FSL.BlazorCookies.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;

namespace FSL.BlazorCookies.Pages
{
    public class LogoutPageModel : PageModel
    {
        private readonly IAuthenticationService _authenticationService;

        public LogoutPageModel(
            IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        public async Task<IActionResult> OnGetAsync(
            bool login = false)
        {
            await _authenticationService.LogoutAsync();

            if (login)
            {
                return LocalRedirect(Url.Content("~/logincontrol"));
            }

            return LocalRedirect(Url.Content("~/"));
        }
    }
}
