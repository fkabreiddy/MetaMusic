using MetaMusic.Data.OtherEntities;
using MetaMusic.Data.Request;
using MetaMusic.Data.Responses;

namespace MetaMusic.Data.Services
{
    public interface IUserService
    {
        Task<Result<UsuarioResponse>> ConsultarUsuario(string email);
        Task<Result<UsuarioResponse>> ConsultarUsuarioActual();
        Task<Result<UsuarioResponse>> Crear(UsuarioRequest request);
        Task<Result<List<UsuarioResponse>>> BuscarUsuario(string filtro);
        Task<Result<bool>> Eliminar(UsuarioRequest request);
        Task<Result<UsuarioResponse>> Login(UsuarioRequest request);
        Task<Result> Logout();
        Task<Result<UsuarioResponse>> Modificar(UsuarioRequest request);
    }
}