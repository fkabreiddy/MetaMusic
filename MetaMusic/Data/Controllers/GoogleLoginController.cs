using MetaMusic.Data.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using EmbedIO.Routing;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components.Authorization;
using MetaMusic.Data.OtherEntities;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using Microsoft.Win32;

namespace MetaMusic.Data.Controllers
{
    [Microsoft.AspNetCore.Mvc.Route("/[controller]")]
    [ApiController]
    public class GoogleLoginController : ControllerBase
    {
        private readonly IAsignDataService asingData;
        private readonly NavigationManager navManager;

        private readonly IUserService userService;
        private readonly HttpClient httpClient;
        private readonly ICurrentUser currentUser;


        public GoogleLoginController(IAsignDataService asingData, NavigationManager navManager, HttpClient httpClient, IUserService userService, ICurrentUser currentUser)
        {
            this.asingData = asingData;
            this.navManager = navManager;
            this.userService = userService;
            this.currentUser = currentUser;
            this.httpClient = httpClient;
            
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


                    var email = GoogleUser.FindFirst(c => c.Type == ClaimTypes.Email)?.Value;

                    if(email is null)
                    {
                        return LocalRedirect("/login-error");
                    }

                    var user = await userService.Login(email);

                    if (user.Data is null)
                    {
                        var nombre = GoogleUser.FindFirst(c => c.Type == ClaimTypes.GivenName)?.Value ?? "John";

                        var apellido = GoogleUser.FindFirst(c => c.Type == ClaimTypes.Surname)?.Value ?? "Doe";

                        var avatar = GoogleUser.FindFirst(c => c.Type == "picture")?.Value;
                        var registar = await userService.Crear(nombre + " " + apellido, email, avatar);

                        if (registar.Success && registar.Data is not null)
                        {
                            var claimsprincipal = CreateClaims(registar.Data.ToLoginResponse());
                            var authenticationProperties = new AuthenticationProperties
                            {
                                AllowRefresh = false,
                                IsPersistent = true,
                            };
                            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimsprincipal, authenticationProperties);
                            return LocalRedirect("/");
                        }
                    }


                    var claimsprincipal2 = CreateClaims(user.Data);
                    var authenticationProperties2 = new AuthenticationProperties
                    {
                        AllowRefresh = false,
                        IsPersistent = true,
                    };
                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimsprincipal2, authenticationProperties2);
                    return LocalRedirect("/");

                    

                   


                }
                else
                {
                    return LocalRedirect("login-error");
                }

                
            }
            catch(Exception ex)
            {
                return LocalRedirect("/login-error");
            }
        }

        [HttpGet("logout/")]
        public async Task<IActionResult> logOut(
           string returnUrl = "")
        {
            returnUrl = returnUrl ?? Url.Content("~/");
            // Clear the existing external cookie
            try
            {

                var authenticationProperties = new AuthenticationProperties
                {
                    AllowRefresh = false,
                    IsPersistent = true,
                };

                await HttpContext.SignOutAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme, authenticationProperties);
               
                                HttpContext.Session.Clear();

              



            }
            catch (Exception ex)
            {
                string error = ex.Message;
            }

            return LocalRedirect("/");
        }


        private ClaimsPrincipal CreateClaims(LoginResponse user)
        {
            var claimsPrincipal = new ClaimsPrincipal(new ClaimsIdentity(new List<Claim>
                {
                        new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                        new Claim(ClaimTypes.Name, user.Nombre),
                        new Claim(ClaimTypes.Role, user.Rol.Tipo),
                        new Claim(ClaimTypes.Email, user.Correo),


                },  CookieAuthenticationDefaults.AuthenticationScheme));


            return claimsPrincipal;
        }
        
    }
}
