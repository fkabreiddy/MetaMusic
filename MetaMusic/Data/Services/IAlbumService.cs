using MetaMusic.Data.Entities;
using MetaMusic.Data.OtherEntities;
using MetaMusic.Data.Request;
using MetaMusic.Data.Responses;

namespace MetaMusic.Data.Services
{
    public interface IAlbumService
    {
        Task<Result<List<AlbumResponse>>> ConsultarMisAlbumes(UsuarioResponse user);
        Task<Result<List<AlbumResponse>>> ConsultarRecientes();
        Task<Result<AlbumResponse>> ConsultarUno(string spotifyId);
        Task<Result<AlbumResponse>> Crear(AlbumRequest request, ReviewRequest review);
        Task<Result<Usuario_Like_Track>> DisLikeTrack(Track track);
        Task<Result<bool>> Eliminar(AlbumResponse response);
        Task<Result<List<Track>>> GetTrackData(AlbumResponse album);
        Task<Result<Usuario_Like_Track>> LikeTrack(Track track);
        Task<Result<AlbumResponse>> Modificar(AlbumRequest request, ReviewRequest review);

        Task<Result<List<AlbumResponse>>> ConsultarPorArtista(int artistaid);
        Task<Result<AlbumResponse>> GetBestReview();
    }
}