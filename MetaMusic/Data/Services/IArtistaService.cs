using MetaMusic.Data.OtherEntities;
using MetaMusic.Data.Request;
using MetaMusic.Data.Responses;

namespace MetaMusic.Data.Services
{
    public interface IArtistaService
    {
        Task<Result<ArtistaResponse>> ConsultarArtista(string spotifyId);
        Task<Result<List<ArtistaResponse>>> ConsultarTodosLosArtistas();
        Task<Result<ArtistaResponse>> CrearArtista(ArtistaRequest request);
        Task<Result<ArtistaResponse>> Eliminar(ArtistaRequest request);
        Task<Result<ArtistaResponse>> ModificarArtista(ArtistaRequest request);
    }
}