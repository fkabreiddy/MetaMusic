using MetaMusic.Data.OtherEntities;
using MetaMusic.Data.Request;
using MetaMusic.Data.Responses;

namespace MetaMusic.Data.Services
{
    public interface IUserService
    {
        Task<Result<UsuarioResponse>> Crear(string email, string fotoperfil, string nombre);
        Task<Result<bool>> Eliminar(UsuarioRequest request);
        Task<Result<UsuarioResponse>> Login(string email);
        Task<Result> Logout();
        Task<Result<UsuarioResponse>> Modificar(UsuarioRequest request);
    }
}