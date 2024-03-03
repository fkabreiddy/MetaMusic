using MetaMusic.Data.OtherEntities;

namespace MetaMusic.Data.Services
{
    public interface IPeticionService
    {
        Task<Result<bool>> CrearAlbum(string spotifyId);
        Task<Result<bool>> CrearArtista(string spotifyId);
        Task<Result<bool>> Eliminar(int id);
    }
}