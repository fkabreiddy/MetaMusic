using MetaMusic.Data.OtherEntities;

namespace MetaMusic.Data.Services
{
    public interface IGoogleAuthService
    {
        Task<Result<LoginResponse>> GetCurrentUser();
        
    }
}