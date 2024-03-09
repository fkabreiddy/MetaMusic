using MetaMusic.Data.Entities;
using MetaMusic.Data.OtherEntities;

namespace MetaMusic.Data.Services
{
    public interface IStatsService
    {
        Task<Result<Dictionary<Genero, int>>> RankGenerosAlbumes();
        Task<Result<Dictionary<Genero, int>>> RankGenerosArtistas();
        Task<Result<Dictionary<string, int>>> SinglesAndAlbums();
    }
}