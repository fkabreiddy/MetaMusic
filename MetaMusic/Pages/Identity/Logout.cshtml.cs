using MetaMusic.Authentication;
using MetaMusic.Data.Services;
using MetaMusic.Data.OtherEntities;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;


namespace MetaMusic.Pages.Identity
{
    public class LogoutModel : PageModel
    {
        private readonly ICustomAuthenticationStateProvider customAuthState;
        public string ReturnUrl { get; private set; }

        public LogoutModel(ICustomAuthenticationStateProvider customAuthState)
        {
            this.customAuthState = customAuthState;
            
        }
        public async Task<IActionResult> OnGetAsync(
            string returnUrl = null)
        {
            returnUrl = returnUrl ?? Url.Content("~/");
            // Clear the existing external cookie
            try
            {
                await HttpContext
                    .SignOutAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme);

               
            }
            catch (Exception ex)
            {
                string error = ex.Message;
            }
             
            return LocalRedirect("/");
        }

       
    }
}