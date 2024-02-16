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
                };
                return new ChallengeResult(provider, authenticationProperties);
            }
            catch
            {
                return RedirectToAction("LoginError");
            }
        }

        
        public async Task<IActionResult> Callback(string returnUrl = null, string remoteError = null)
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

                var r = await asingData.AsignData();

                if (r.Success == true)
                {
                    // Agregar el claim de rol a la identidad del usuario
                    var roleClaim = new Claim(ClaimTypes.Role, r.Data.Rol.Tipo);
                    GoogleUser.AddClaim(roleClaim);

                    // Actualizar la cookie de autenticación
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
                    return LocalRedirect("/google-log-out");
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
