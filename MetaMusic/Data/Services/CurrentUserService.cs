using MetaMusic.Data.Responses;

namespace MetaMusic.Data.Services
{
    public class CurrentUser : ICurrentUser
    {
        public UsuarioResponse UsuarioActual { get; set; } = new UsuarioResponse();

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
