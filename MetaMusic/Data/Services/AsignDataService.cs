using MetaMusic.Data.Request;
using MetaMusic.Data.Services;
using MetaMusic.Pages;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using System.Security.Claims;

namespace MetaMusic.Data.Services
{
    public class AsignDataService : IAsignDataService
    {

        private readonly IHttpContextAccessor Context;
        private readonly IUserService userServices;
        private readonly NavigationManager navigationManager;
        private readonly IUserDataService userDataService;
        public AsignDataService(IUserDataService userDataService,IHttpContextAccessor Context, IUserService userServices, NavigationManager navigationManager)
        {

            this.Context = Context;
            this.userServices = userServices;
            this.navigationManager = navigationManager;
           
            this.userDataService = userDataService;
        }

        public async Task AsignarDatos()
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
                  .FindFirst(ClaimTypes.Email);

                if (email != null)
                {
                    Gmail = email.Value;
                }
                else
                {
                    Gmail = User?.Identity?.Name;
                }

                var givenName =
                    Context.HttpContext?.User
                    .FindFirst(ClaimTypes.GivenName);
                if (givenName != null)
                {
                    GivenName = givenName.Value;
                }
                else
                {
                    GivenName = User?.Identity?.Name;
                }

                // Try to get the Surname
                var surname =
                    Context.HttpContext?.User
                    .FindFirst(ClaimTypes.Surname);
                if (surname != null)
                {
                    Surname = surname.Value;
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
                      var creacion =   await userServices.Crear(Gmail, avatar ?? "", givenName + " " + surname);

                        if(creacion.Success && creacion.Data is not null)
                        {
                            userDataService.UpdateUserData(creacion.Data);
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
