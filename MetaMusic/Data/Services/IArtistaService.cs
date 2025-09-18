using MetaMusic.Data.OtherEntities;
using MetaMusic.Data.Request;
using MetaMusic.Data.Responses;

namespace MetaMusic.Data.Services
{
    public interface IArtistaService
    {
        Task<Result<ArtistaResponse>> ConsultarArtista(string spotifyId);
        Task<Result<List<ArtistaResponse>>> ConsultarTodosLosArtistas(int cantidad);
        Task<Result<ArtistaResponse>> CrearArtista(ArtistaRequest request);
        Task<Result<ArtistaResponse>> DesSuscribirse(ArtistaResponse artista);
        Task<Result<ArtistaResponse>> Eliminar(ArtistaRequest request);
        Task<Result<ArtistaResponse>> ModificarArtista(ArtistaRequest request);
        Task<Result<ArtistaResponse>> Suscribirse(ArtistaResponse artista);
        Task<Result<List<ArtistaResponse>>> BuscarVarios(string nombre);
        Task<Result<List<ArtistaResponse>>> ConsultarTodosLosArtistas();
        Task<Result<List<ArtistaResponse>>> ConsultarMas(int startindex);
        Task<Result<List<ArtistaResponse>>> ArtstasSuscrito(UsuarioResponse usuario);
    }
}