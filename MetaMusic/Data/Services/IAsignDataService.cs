
using MetaMusic.Data.OtherEntities;

namespace MetaMusic.Data.Services
{
    public interface IAsignDataService
    {
        Task<Result<LoginResponse>> AsignData();
    }
}