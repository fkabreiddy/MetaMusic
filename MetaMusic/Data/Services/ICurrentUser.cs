using MetaMusic.Data.Responses;
using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Principal;

namespace MetaMusic.Data.Services
{
    public interface ICurrentUser
    {
        UsuarioResponse GetUsuarioActual();
        bool SetUsuarioActual(UsuarioResponse response);

        Task<AuthenticationState> GetIdentity();
    }
}