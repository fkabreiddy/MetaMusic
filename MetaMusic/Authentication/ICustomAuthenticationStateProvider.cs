using Microsoft.AspNetCore.Components.Authorization;

namespace MetaMusic.Authentication
{
    public interface ICustomAuthenticationStateProvider
    {
        Task<AuthenticationState> GetAuthenticationStateAsync();
    }
}