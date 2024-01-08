using MetaMusic.Data.OtherEntities;
using MetaMusic.Data.Responses;

namespace MetaMusic.Data.Services
{
    public interface ISpotifyService
    {
        Task<Result<AlbumResponse>> GetAlbum(string albumId);
        Task<Result<ArtistaResponse>> GetArtista(string artistsId);
    }
}