namespace MetaMusic.Data.Services
{
    public interface ICurrentUserServices
    {
        Task<string> GivenName();
        Task<string> Name();
        Task<string> Rol();
        Task<int> UserId();
    }
}