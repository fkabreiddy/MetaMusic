using MetaMusic.Data.OtherEntities;
using MetaMusic.Data.Request;
using MetaMusic.Data.Responses;

namespace MetaMusic.Data.Services
{
    public interface IUserService
    {
        Task<Result<List<UsuarioResponse>>> BuscarUsuario(string filtro);
        Task<Result<UsuarioResponse>> ConsultarUsuario(string email);
        Task<Result<UsuarioResponse>> ConsultarUsuarioActual();
        Task<Result<UsuarioResponse>> Crear(string nombre, string correo, string avatar);
        Task<Result<bool>> Eliminar(UsuarioRequest request);
        Task<Result<LoginResponse>> Login(string email);
        Task<Result<bool>> Logout();
        Task<Result<UsuarioResponse>> Modificar(UsuarioRequest request);
    }
}