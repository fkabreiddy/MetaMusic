using MetaMusic.Data.Entities;
using MetaMusic.Data.OtherEntities;
using MetaMusic.Data.Responses;

namespace MetaMusic.Data.Services
{
    public interface IStatsService
    {
        Task<Result<List<AlbumResponse>>> AlbumesPerYear(int year);
        Task<Result<Dictionary<Genero, int>>> RankGenerosArtistas();
        Task<Result<Dictionary<string, int>>> SinglesAndAlbums();

        Task<Result<Dictionary<UsuarioResponse, int>>> TopMostActiveUsers(int year);
        Task<Result<List<AlbumResponse>>> MyAlbums(int year);
    }
}