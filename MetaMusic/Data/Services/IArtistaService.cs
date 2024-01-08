using MetaMusic.Data.OtherEntities;
using MetaMusic.Data.Request;
using MetaMusic.Data.Responses;

namespace MetaMusic.Data.Services
{
    public interface IArtistaService
    {
        Task<Result<ArtistaResponse>> CrearArtista(ArtistaRequest request);
    }
}