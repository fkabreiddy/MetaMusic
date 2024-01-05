using MetaMusic.Data.Entities;
using MetaMusic.Data.Responses;

namespace MetaMusic.Data.Services
{
    public class UserDataService : IUserDataService
    {
        public UsuarioResponse UsuarioActual { get; set; } = new UsuarioResponse();


        public void UpdateUserData(UsuarioResponse usuario)
        {
            UsuarioActual = usuario;
        }

        public void DelteUserData()
        {
            UsuarioActual = new UsuarioResponse();
        }


        public UsuarioResponse GetCurrentUserData() { return UsuarioActual; }
    }
}
