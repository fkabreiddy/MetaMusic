using MetaMusic.Data.Responses;

namespace MetaMusic.Data.Services
{
    public interface ICurrentUser
    {
        UsuarioResponse GetUsuarioActual();
        bool SetUsuarioActual(UsuarioResponse response);
    }
}