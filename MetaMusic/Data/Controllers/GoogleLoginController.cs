using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Linq;
using MetaMusic.Data.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Authorization;
using MetaMusic.Data.OtherEntities;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authentication.OAuth;
using Microsoft.AspNetCore.Http;

[Microsoft.AspNetCore.Mvc.Route("auth/[controller]")]
[ApiController]
public class GoogleLoginController : ControllerBase
{
    private readonly IAsignDataService asingData;
    private readonly NavigationManager navManager;
    private readonly IUserService userService;
    private readonly ICurrentUser currentUser;

    public GoogleLoginController(IAsignDataService asingData, NavigationManager navManager, IUserService userService, ICurrentUser currentUser)
    {
        this.asingData = asingData;
        this.navManager = navManager;
        this.userService = userService;
        this.currentUser = currentUser;
    }

    [AllowAnonymous]
    [HttpGet("signin-google")]
    public async Task Login(string returnUrl = "")
    {
        try
        {
            var authenticationProperties = new AuthenticationProperties
            {
                RedirectUri = "https://localhost:7277/auth/googlelogin/complete",
                IsPersistent = true,
                
            };



            await this.HttpContext.ChallengeAsync(GoogleDefaults.AuthenticationScheme, authenticationProperties );
        }
        catch
        {
            navManager.NavigateTo("/login-error");
        }
    }


    [HttpGet("complete")]
    public async Task<IActionResult> Callback(string returnUrl = "", string remoteError = "")
    {
        try
        {
            var GoogleUser = this.User.Identities.FirstOrDefault();
            if (GoogleUser != null && GoogleUser.IsAuthenticated)
            {
                var email = GoogleUser.FindFirst(c => c.Type == ClaimTypes.Email)?.Value;

                if (email == null)
                {
                    return LocalRedirect("/login-error");
                }

                var user = await userService.Login(email);

                if (user.Data == null)
                {
                    var nombre = GoogleUser.FindFirst(c => c.Type == ClaimTypes.GivenName)?.Value ?? "John";
                    var apellido = GoogleUser.FindFirst(c => c.Type == ClaimTypes.Surname)?.Value ?? "Doe";
                    var avatar = GoogleUser.FindFirst(c => c.Type == "picture")?.Value;
                    var registrar = await userService.Crear(nombre + " " + apellido, email, avatar ?? "");

                    if (registrar.Success && registrar.Data != null)
                    {
                        var claimsPrincipal = CreateClaims(registrar.Data.ToLoginResponse());
                        var authenticationProperties = new AuthenticationProperties
                        {
                            AllowRefresh = false,
                            IsPersistent = true,
                        };
                        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimsPrincipal, authenticationProperties);
                        return LocalRedirect("/" + returnUrl);
                    }
                }

                if (user.Data != null)
                {
                    var claimsPrincipal2 = CreateClaims(user.Data);
                    var authenticationProperties2 = new AuthenticationProperties
                    {
                        AllowRefresh = false,
                        IsPersistent = true,
                    };
                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimsPrincipal2, authenticationProperties2);
                    return LocalRedirect("/"+returnUrl);
                }

                return LocalRedirect("/login-error");
            }
            else
            {
                return LocalRedirect("/login-error");
            }
        }
        catch
        {
            return LocalRedirect("/login-error");
        }
    }

    [HttpGet("logout")]
    public async Task<IActionResult> Logout(string returnUrl = "")
    {
        returnUrl = returnUrl ?? Url.Content("~/");
        try
        {


            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
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
        }, CookieAuthenticationDefaults.AuthenticationScheme));

        return claimsPrincipal;
    }
}
