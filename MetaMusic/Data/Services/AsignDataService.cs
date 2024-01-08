using MetaMusic.Data.Context;
using Microsoft.AspNetCore.Components;
using System.Security.Claims;

namespace MetaMusic.Data.Services
{
    public class AsignDataService : IAsignDataService
    {
        private readonly IHttpContextAccessor Context;
        private readonly IUserService userServices;
        private readonly NavigationManager navigationManager;

        private readonly IMetaMusicDbContext dbContext;

        public AsignDataService(IMetaMusicDbContext dbContext, IHttpContextAccessor Context, IUserService userServices, NavigationManager navigationManager)
        {
            this.dbContext = dbContext;
            this.Context = Context;
            this.userServices = userServices;
            this.navigationManager = navigationManager;


        }

        public async Task AsignData()
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

                if (Gmail != "" && Gmail is not null)
                {
                    var r = await userServices.Login(Gmail);
                    if (r.Success == false)
                    {
                        var creacion = await userServices.Crear(Gmail, avatar ?? "", givenName + " " + surname);

                        if (creacion.Success && creacion.Data is not null)
                        {

                            navigationManager.NavigateTo("/", true);

                        }
                    }
                    else if (r.Data is not null && r.Success == true)
                    {

                        navigationManager.NavigateTo("/");


                    }
                }
                else
                {
                    navigationManager.NavigateTo("/", true);
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }
    }
}
