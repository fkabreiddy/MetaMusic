using EmbedIO.Routing;
using MetaMusic.Data.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Security.Claims;

namespace MetaMusic.Data.Controllers
{



    [AllowAnonymous]
    
    public class AccountController : ControllerBase
    {
        
        private readonly IAsignDataService asingData;
              private readonly NavigationManager navManager;

            public AccountController(IAsignDataService asingData, NavigationManager navManager)
            {
                this.asingData = asingData;
              this.navManager = navManager;
            }


        
        public IActionResult Login(string returnUrl = "")
            {
                try
                {
                    if (string.IsNullOrEmpty(returnUrl))
                    {
                        // Si returnUrl está vacío, obtén la URL actual y úsala como returnUrl predeterminado
                        returnUrl = navManager.Uri;
                    }

                    string provider = "Google";
                    var authenticationProperties = new AuthenticationProperties
                    {
                        RedirectUri = Url.Action("Callback", "Account", new { returnUrl }),
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
        }

    
}
