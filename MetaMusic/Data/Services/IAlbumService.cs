using MetaMusic.Data.Entities;
using MetaMusic.Data.OtherEntities;
using MetaMusic.Data.Request;
using MetaMusic.Data.Responses;

namespace MetaMusic.Data.Services
{
    public interface IAlbumService
    {
        Task<Result<List<AlbumResponse>>> BuscarVarios(string filtro, bool just8PlusReviews = false, bool justMustListen = false);
        Task<Result<List<AlbumResponse>>> ConsultarMisAlbumes(UsuarioResponse user);
        Task<Result<List<AlbumResponse>>> ConsultarPorArtista(int artistaid);
        Task<Result<List<AlbumResponse>>> ConsultarRecientes();
        Task<Result<List<AlbumResponse>>> ConsultarRecientesSingles();
        Task<Result<AlbumResponse>> ConsultarUno(string spotifyId);
        Task<Result<AlbumResponse>> ConsultarUnSingle(string spotifyId);
        Task<Result<List<AlbumResponse>>> ConsultarVarios(int cantidad);
        Task<Result<AlbumResponse>> Crear(AlbumRequest request, ReviewRequest review);
        Task<Result<AlbumResponse>> CrearSingle(AlbumRequest request, ReviewRequest review, bool enlazarAlbum);
        Task<Result<Usuario_Like_Track>> DisLikeTrack(Track track);
        Task<Result<bool>> Eliminar(AlbumResponse response);
        Task<Result<List<AlbumResponse>>> GetMore(int startIndex, int cantidad);
        Task<Result<AlbumResponse>> GetSingleReferenceAlbum(int albumId);
        Task<Result<List<Track>>> GetTrackData(AlbumResponse album);
        Task<Result<Usuario_Like_Track>> LikeTrack(Track track);
        Task<Result<AlbumResponse>> Modificar(AlbumRequest request, ReviewRequest review);
        Task<Result<AlbumResponse>> ModificarSingle(AlbumRequest request, ReviewRequest review, bool enlazarAlbum);

        Task<Result<List<AlbumResponse>>> ConsultarPorGenero(string genero);
    }
}