using MetaMusic.Data.Responses;

namespace MetaMusic.Data.Services
{
    public interface IUserDataService
    {
        UsuarioResponse UsuarioActual { get; set; }

        void DelteUserData();
        UsuarioResponse GetCurrentUserData();
        void UpdateUserData(UsuarioResponse usuario);
    }
}