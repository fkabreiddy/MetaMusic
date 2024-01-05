using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using MetaMusic.Data.Responses;
using MetaMusic.Data.OtherEntities;

namespace MetaMusic.Data.Services
{
    public class CurrentUserServices : ICurrentUserServices
    {
        private readonly ProtectedSessionStorage _sessionStorage;
        public CurrentUserServices(ProtectedSessionStorage sessionStorage)
        {
            _sessionStorage = sessionStorage;
        }

        private async Task<LoginResponse> GetUserDataAsync()
        {
            var userDataStorage = await _sessionStorage.GetAsync<LoginResponse>("UserToken");
            return userDataStorage!.Success ? userDataStorage!.Value! : new LoginResponse();
        }
        public async Task<int> UserId()
        {
            var userData = await GetUserDataAsync();
            return userData?.Id ?? 0;
        }

        public async Task<string> GivenName()
        {
            var userData = await GetUserDataAsync();
            return userData?.Nombre ?? "Anonimo";
        }
        public async Task<string> Name()
        {
            var userData = await GetUserDataAsync();
            return userData?.Correo ?? "Guest";

        }

        public async Task<string> Rol()
        {
            var userData = await GetUserDataAsync();
            return userData?.Rol.Tipo ?? "Normal";

        }

    }

}