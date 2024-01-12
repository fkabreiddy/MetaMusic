using MetaMusic.Data.Context;
using MetaMusic.Data.Request;
using Microsoft.AspNetCore.Components;
using System.Security.Claims;

namespace MetaMusic.Data.Services
{
    public class AsignDataService : IAsignDataService
    {
        private readonly IHttpContextAccessor Context;
        private readonly IUserService userServices;
        private readonly NavigationManager navigationManager;
        private readonly IGoogleAuthService authService;
        private readonly IMetaMusicDbContext dbContext;

        public AsignDataService(IGoogleAuthService authService, IMetaMusicDbContext dbContext, IHttpContextAccessor Context, IUserService userServices, NavigationManager navigationManager)
        {
            this.dbContext = dbContext;
            this.Context = Context;
            this.userServices = userServices;
            this.navigationManager = navigationManager;
            this.authService = authService;

          


        }

        public async Task<bool> AsignData()
        {
            try
            {
                ClaimsPrincipal? User = new ClaimsPrincipal();
                string? GivenName = "";
                string? Surname = "";
                string? Avatar = "";
                string? Gmail = "";

                User = Context.HttpContext?.User;

                var email =
                  Context.HttpContext?.User
                  .FindFirst(ClaimTypes.Email)?.Value;

                if (email != null)
                {
                    Gmail = email;
                }
                else
                {
                    Gmail = User?.Identity?.Name;
                }

                var givenName =
                    Context.HttpContext?.User
                    .FindFirst(ClaimTypes.GivenName)?.Value;
                if (givenName != null)
                {
                    GivenName = givenName;
                }
                else
                {
                    GivenName = User?.Identity?.Name;
                }

                // Try to get the Surname
                var surname =
                    Context.HttpContext?.User
                    .FindFirst(ClaimTypes.Surname)?.Value;
                if (surname != null)
                {
                    Surname = surname;
                }
                else
                {
                    Surname = "";
                }

                // Try to get Avatar
                var avatar =
                Context.HttpContext?.User.Claims?.Where(s => s.Type == "picture")
                .FirstOrDefault()?.Value;
                if (avatar != null)
                {
                    Avatar = avatar;
                }
                else
                {
                    Avatar = "";
                }


                UsuarioRequest request = new UsuarioRequest();
                if (Gmail != "" && Gmail is not null)
                {

                    request.Biografia = $"Mi nombre es {GivenName}";
                    request.Correo = Gmail;
                    request.Nombre = GivenName + " "+ Surname;
                    request.FotoDePerfil = Avatar;
                    string Normalizar( string cadena)
                    {
                        // Encuentra la posición del primer arroba
                        int indiceArroba = cadena.IndexOf('@');

                        if (indiceArroba != -1) // Verifica si se encontró el arroba
                        {
                            // Extrae la subcadena hasta el arroba
                            return cadena.Substring(0, indiceArroba);
                        }
                        else
                        {
                            // Si no se encontró el arroba, devuelve la cadena completa
                            return cadena;
                        }
                    }
                    request.CorreoNormalizado = Normalizar(Gmail);
                   var r = await userServices.Login(request);

                    if(r.Success)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                   
                }
                else
                {

                    return false;
                }

            }
            catch (Exception ex)
            {
                return false;
            }

        }
    }
}
