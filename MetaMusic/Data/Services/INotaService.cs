using MetaMusic.Data.OtherEntities;
using MetaMusic.Data.Request;
using MetaMusic.Data.Responses;

namespace MetaMusic.Data.Services
{
    public interface INotaService
    {
        Task<Result<NotaResponse>> Crear(NotaRequest request, CalificacionRequest calificacionrequest, AlbumRequest albumrequest);
    }
}