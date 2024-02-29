using MetaMusic.Data.Responses;
using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Principal;

namespace MetaMusic.Data.Services
{
    public class CurrentUser : ICurrentUser
    {
        public UsuarioResponse UsuarioActual { get; set; } = new UsuarioResponse();

        private readonly AuthenticationStateProvider _authenticationStateProvider;

        public CurrentUser(AuthenticationStateProvider authenticationStateProvider)
        {
            _authenticationStateProvider = authenticationStateProvider;
        }

        public async Task<AuthenticationState> GetIdentity()
        {
            var authState = await _authenticationStateProvider.GetAuthenticationStateAsync();
           
            return authState;
        }
        public UsuarioResponse GetUsuarioActual()
        {
            return UsuarioActual;
        }

        public bool SetUsuarioActual(UsuarioResponse response)
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
    }
}
