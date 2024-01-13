using MetaMusic.Data.OtherEntities;
using MetaMusic.Data.Request;
using MetaMusic.Data.Responses;

namespace MetaMusic.Data.Services
{
    public interface IAlbumService
    {
        Task<Result<AlbumResponse>> ConsultarUno(string spotifyId);
        Task<Result<AlbumResponse>> Crear(AlbumRequest request, CalificacionRequest calificacion, ReviewRequest review);
    }
}