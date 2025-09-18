using MetaMusic.Data.OtherEntities;
using MetaMusic.Data.Request;
using MetaMusic.Data.Responses;

namespace MetaMusic.Data.Services
{
    public interface INotaService
    {
        Task<Result<List<NotaResponse>>> Consultar(int albumid);
        Task<Result<NotaResponse>> Crear(NotaRequest request, AlbumRequest albumrequest);
        Task<Result<NotaResponse>> DisLikeNota(int notaid, int userid);
        Task<Result<bool>> Eliminar(int notaid);
        Task<Result<NotaResponse>> LikeNota(int notaid, int userid);
        Task<Result<List<NotaResponse>>> ConsulatNotasDelUsuario(int userid);
    }
}