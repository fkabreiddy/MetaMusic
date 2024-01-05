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
        private readonly IUserDataService userDataService;
        public string ReturnUrl { get; private set; }

        public LogoutModel(IUserDataService userDataService,ICustomAuthenticationStateProvider customAuthState)
        {
            this.customAuthState = customAuthState;
            this.userDataService = userDataService;
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

                await Logout();
            }
            catch (Exception ex)
            {
                string error = ex.Message;
            }
             
            return LocalRedirect("/");
        }

        public async Task<Result> Logout()
        {
            try
            {


                userDataService.DelteUserData();
                await customAuthState.UpdateAuthenticationState(null!);
               

                return new Result()
                {

                    Message = "DesLogeo Exitoso",
                    Success = true
                };
            }
            catch (Exception E)
            {
                return new Result()
                {

                    Message = E.InnerException?.Message ?? E.Message,
                    Success = false
                };
            }
        }
    }
}