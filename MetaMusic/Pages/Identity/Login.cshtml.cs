using MetaMusic.Data.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;

namespace MetaMusic.Pages.Identity
{
    [AllowAnonymous]
    public class LoginModel : PageModel
    {
        private readonly IAsignDataService asingData;
        private readonly NavigationManager navManager;

        public LoginModel(IAsignDataService asingData, NavigationManager navManager)
        {
            this.asingData = asingData;
            this.navManager = navManager;
        }
        public IActionResult OnGetAsync(string returnUrl = "")
        {
            try
            {

              

                string provider = "Google";
                // Request a redirect to the external login provider.
                var authenticationProperties = new AuthenticationProperties
                {
                    RedirectUri = Url.Page("./Login",
                    pageHandler: "Callback",
                    values: new { returnUrl }),
                };

               
                return new ChallengeResult(provider, authenticationProperties);
            }
            catch
            {

                return LocalRedirect("/login-error");
            }
           
        }
        public async Task<IActionResult> OnGetCallbackAsync( string returnUrl = null, string remoteError = null)
        {
            try
            {
                var GoogleUser = this.User.Identities.FirstOrDefault();
                if (GoogleUser.IsAuthenticated)
                {
                    var authProperties = new AuthenticationProperties
                    {
                        IsPersistent = true,
                        RedirectUri = this.Request.Host.Value
                    };

                    
                    await HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(GoogleUser));


                }

               var r =  await asingData.AsignData();

                if(r == true)
                {
                    return LocalRedirect("/");
                }
                else
                {
                    return LocalRedirect("/google-log-out");
                }
                
            }
            catch
            {
                return LocalRedirect("/login-error");
            }
            
          
        }
    }
}