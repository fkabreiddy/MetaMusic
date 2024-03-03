using MetaMusic.Data.OtherEntities;
using MetaMusic.Data.Responses;

namespace MetaMusic.Data.Services
{
    public interface ICurrentUser
    {
        LoginResponse UsuarioActual { get; set; }

        LoginResponse GetUsuarioActual();
        bool SetUsuarioActual(UsuarioResponse response);

        bool SetUsuarioActual(LoginResponse response);
    }
}