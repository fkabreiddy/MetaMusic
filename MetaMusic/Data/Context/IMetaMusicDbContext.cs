
namespace MetaMusic.Data.Context
{
    public interface IMetaMusicDbContext
    {
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}