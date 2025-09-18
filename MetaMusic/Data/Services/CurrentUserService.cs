using MetaMusic.Data.OtherEntities;
using MetaMusic.Data.Responses;
using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Principal;

namespace MetaMusic.Data.Services
{
    public class CurrentUser : ICurrentUser
    {
        public LoginResponse UsuarioActual { get; set; } = new LoginResponse();

        private readonly AuthenticationStateProvider _authenticationStateProvider;

        public CurrentUser(AuthenticationStateProvider authenticationStateProvider)
        {
            _authenticationStateProvider = authenticationStateProvider;
        }


        public LoginResponse GetUsuarioActual()
        {
            return UsuarioActual;
        }

        public bool SetUsuarioActual(LoginResponse response)
        {
            if (response is not null)
            {
                UsuarioActual = response;
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool SetUsuarioActual(UsuarioResponse response)
        {
            if (response is not null)
            {
                UsuarioActual = response.ToLoginResponse();
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
