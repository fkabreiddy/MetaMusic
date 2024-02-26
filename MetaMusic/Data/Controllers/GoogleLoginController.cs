using MetaMusic.Data.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using EmbedIO.Routing;
using Microsoft.AspNetCore.Authorization;

namespace MetaMusic.Data.Controllers
{
    [Microsoft.AspNetCore.Mvc.Route("/[controller]")]
    [ApiController]
    public class GoogleLoginController : ControllerBase
    {
        private readonly IAsignDataService asingData;
        private readonly NavigationManager navManager;
     

        public GoogleLoginController(IAsignDataService asingData, NavigationManager navManager)
        {
            this.asingData = asingData;
            this.navManager = navManager;
            
        }

        [AllowAnonymous]
        [HttpGet("signin-google")]

        public IActionResult Login(string returnUrl = "")
        {
            try
            {
                
                
                string provider = "Google";
                var authenticationProperties = new AuthenticationProperties
                {
                    RedirectUri = Url.Action("Callback", "GoogleLogin", new { returnUrl }),
                    IsPersistent= true,
                };
                return new ChallengeResult(provider, authenticationProperties);
            }
            catch
            {
                return LocalRedirect("/LoginError");
            }
        }

        
        public async Task<IActionResult> Callback(string returnUrl = null, string remoteError = null)
        {
            try
            {
                var GoogleUser = this.User.Identities.FirstOrDefault();
                if (GoogleUser.IsAuthenticated)
                {
                    var r = await asingData.AsignData();

                    if (r.Success == true && r.Data is not null)
                    {
                        // Agregar el claim de rol a la identidad del usuario
                        var roleClaim = new Claim(ClaimTypes.Role, r.Data.Rol.Tipo );
                        GoogleUser.AddClaim(roleClaim);

                        var authProperties = new AuthenticationProperties
                        {
                            IsPersistent = true,
                            RedirectUri = this.Request.Host.Value
                        };
                        await HttpContext.SignInAsync(
                      CookieAuthenticationDefaults.AuthenticationScheme,
                      new ClaimsPrincipal(GoogleUser),
                      authProperties);

                        return LocalRedirect("/");
                    }
                    else
                    {
                        return LocalRedirect("/login-error");
                    }
                   

                   

                }
                else
                {
                    return LocalRedirect("login-error");
                }

                
            }
            catch
            {
                return RedirectToAction("LoginError");
            }
        }

        [HttpGet("logout")]
        public async Task<IActionResult> logOut(
           string returnUrl = null)
        {
            returnUrl = returnUrl ?? Url.Content("~/");
            // Clear the existing external cookie
            try
            {

               
                await HttpContext.SignOutAsync(
    CookieAuthenticationDefaults.AuthenticationScheme);
                HttpContext.Session.Clear();

               



            }
            catch (Exception ex)
            {
                string error = ex.Message;
            }

            return LocalRedirect("/");
        }


    }
}
