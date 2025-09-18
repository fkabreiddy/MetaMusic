using MetaMusic.Data.OtherEntities;
using MetaMusic.Data.Responses;

namespace MetaMusic.Data.Services
{
    public interface ISpotifyService
    {
        Task<Result<MetaMusic.Data.Request.AlbumRequest>> GetAlbum(string albumId);
        Task<Result<ArtistaResponse>> GetArtista(string artistsId);
        Task<Result<MetaMusic.Data.Request.AlbumRequest>> GetSingle(string albumId);
    }
}