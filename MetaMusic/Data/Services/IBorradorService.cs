using MetaMusic.Data.OtherEntities;
using MetaMusic.Data.Request;
using MetaMusic.Data.Responses;

namespace MetaMusic.Data.Services
{
    public interface IBorradorService
    {
        Task<Result<List<AlbumResponse>>> ConsultarMisBorradoes(UsuarioResponse user);
        Task<Result<AlbumResponse>> CrearBorrador(AlbumRequest request, CalificacionRequest calificacion, ReviewRequest review);
        Task<Result<bool>> Eliminar(AlbumResponse response);
        Task<Result<AlbumResponse>> Modificar(AlbumRequest request, CalificacionRequest calificacion, ReviewRequest review);
    }
}