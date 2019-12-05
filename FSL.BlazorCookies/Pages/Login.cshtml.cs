using FSL.BlazorCookies.Models;
using FSL.BlazorCookies.Provider;
using FSL.BlazorCookies.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;

namespace FSL.BlazorCookies.Pages
{
    [AllowAnonymous]
    public class LoginPageModel : PageModel
    {
        private readonly IAuthenticationService _authenticationService;
        private readonly Service.IAuthorizationService _authorizationService;
        private readonly ICryptographyProvider _cryptographyProvider;
        private readonly string _loginControlName;

        public LoginPageModel(
            IAuthenticationService authenticationService,
            Service.IAuthorizationService authorizationService,
            ICryptographyProvider cryptographyProvider)
        {
            _authenticationService = authenticationService;
            _authorizationService = authorizationService;
            _cryptographyProvider = cryptographyProvider;
            _loginControlName = typeof(LoginControl).Name.ToString();
        }

        public async Task<IActionResult> OnGetAsync(
            string emailOrLogin,
            string password,
            string returnUrl = null)
        {
            emailOrLogin = _cryptographyProvider.DeCrypt(emailOrLogin);
            password = _cryptographyProvider.DeCrypt(password);

            var loginUser = new LoginUser
            {
                LoginOrEmail = emailOrLogin,
                Password = password
            };

            var authorization = await _authorizationService.AuthorizeAsync(loginUser);
            if (!authorization.Success)
            {
                return RedirectToLogin();
            }

            var authentication = await _authenticationService.AuthenticateAsync(authorization.Data);
            if (!authentication.Success)
            {
                return RedirectToLogin();
            }

            return RedirectTo(returnUrl);
        }

        private IActionResult RedirectTo(
            string returnUrl)
        {
            if (!string.IsNullOrEmpty(returnUrl))
            {
                if (returnUrl.Contains(_loginControlName))
                {
                    returnUrl = Url.Content("~/");
                }
                else
                {
                    returnUrl = returnUrl ?? Url.Content("~/");
                }
            }

            return Redirect(returnUrl);
        }

        private IActionResult RedirectToLogin()
        {
            return RedirectTo(Url.Content($"~/{_loginControlName}"));
        }
    }
}
