using MetaMusic.Data.Entities;
using MetaMusic.Data.OtherEntities;
using MetaMusic.Data.Request;
using MetaMusic.Data.Responses;

namespace MetaMusic.Data.Services
{
    public interface IAlbumService
    {
        Task<Result<AlbumResponse>> ConsultarUno(string spotifyId);
        Task<Result<AlbumResponse>> Crear(AlbumRequest request, CalificacionRequest calificacion, ReviewRequest review);
        Task<Result<Track>> DisLikeTrack(Track track);
        Task<Result<List<Track>>> GetTrackData(AlbumResponse album);
        Task<Result<Track>> LikeTrack(Track track);
    }
}