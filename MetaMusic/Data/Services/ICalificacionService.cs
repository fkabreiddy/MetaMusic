using MetaMusic.Data.OtherEntities;
using MetaMusic.Data.Request;
using MetaMusic.Data.Responses;

namespace MetaMusic.Data.Services
{
    public interface ICalificacionService
    {
        Task<Result<List<CalificacionResponse>>> Consultar(AlbumRequest albumrequest);
        Task<Result<CalificacionResponse>> Crear(CalificacionRequest calificacionrequest, AlbumRequest albumrequest);
        Task<Result<bool>> Eliminar(int albumid, int usuarioid);
    }
}