
using MetaMusic.Data.OtherEntities;
using Microsoft.AspNetCore.Components.Authorization;

namespace MetaMusic.Authentication
{
    public interface ICustomAuthenticationStateProvider
    {
        Task<AuthenticationState> GetAuthenticationStateAsync();
        Task UpdateAuthenticationState(LoginResponse userData);
       
    }
}