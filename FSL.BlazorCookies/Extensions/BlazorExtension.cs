using FSL.BlazorCookies.Models;
using FSL.BlazorCookies.Service;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FSL.BlazorCookies.Extensions
{
    public static class BlazorExtension
    {
        public static IServiceCollection AddCookiesAuthentication(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });
            services
                .AddAuthentication(options =>
                {
                    var scheme = CookieAuthenticationDefaults.AuthenticationScheme;

                    options.DefaultAuthenticateScheme = scheme;
                    options.DefaultSignInScheme = scheme;
                    options.DefaultChallengeScheme = scheme;
                })
                .AddCookie(opt =>
                {
                    opt.Cookie.Name = configuration.GetValue<string>($"{typeof(MySettingsConfiguration).Name}:CookieName");
                });

            return services;
        }

        public static IServiceCollection UseCookiesAuthentication(
            this IServiceCollection services)
        {
            services.AddSingleton<IAuthenticationService, CookiesIdentityAuthenticationService>();

            return services;
        }

        public static IApplicationBuilder UseCookiesAuthentication(
            this IApplicationBuilder app)
        {
            var cookiePolicyOptions = new CookiePolicyOptions
            {
                MinimumSameSitePolicy = SameSiteMode.Lax,
            };

            app.UseCookiePolicy(cookiePolicyOptions);
            app.UseAuthentication();
            app.UseAuthorization();

            return app;
        }
    }
}
