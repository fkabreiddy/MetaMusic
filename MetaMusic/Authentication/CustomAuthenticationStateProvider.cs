
using MetaMusic.Data.OtherEntities;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using System.Security.Claims;


namespace MetaMusic.Authentication
{
    public class CustomAuthenticationStateProvider : AuthenticationStateProvider, ICustomAuthenticationStateProvider
    {

        private readonly ProtectedSessionStorage _sessionStorage;
        private ClaimsPrincipal _anonymus = new ClaimsPrincipal(new ClaimsIdentity());

        public CustomAuthenticationStateProvider(ProtectedSessionStorage _sessionStorage)
        {
            this._sessionStorage = _sessionStorage;
        }
        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            try
            {
                var userDataStorage = await _sessionStorage.GetAsync<LoginResponse>("UserToken");
                var userData = userDataStorage.Success ? userDataStorage.Value : null;
                if (userData == null)
                    return await Task.FromResult(new AuthenticationState(_anonymus));
                var claimsPrincipal = new ClaimsPrincipal(new ClaimsIdentity(new List<Claim>
                {
                        new Claim(ClaimTypes.NameIdentifier, userData.Id.ToString()),
                        new Claim(ClaimTypes.Name, userData.Nombre),
                        new Claim(ClaimTypes.Role, userData.Rol.Tipo),
                        new Claim(ClaimTypes.GivenName, userData.Correo),
                        

                }, "CustomAuth"));
                return await Task.FromResult(new AuthenticationState(claimsPrincipal));
            }
            catch
            {
                return await Task.FromResult(new AuthenticationState(_anonymus));
            }
        }

        public async Task UpdateAuthenticationState(LoginResponse userData)
        {
            ClaimsPrincipal claimsPrincipal;
            if (userData != null)
            {
                await _sessionStorage.SetAsync("UserToken", userData);
                claimsPrincipal = new ClaimsPrincipal(new ClaimsIdentity(new List<Claim>
                    {
                        new Claim(ClaimTypes.NameIdentifier, userData.Id.ToString()),
                        new Claim(ClaimTypes.Email, userData.Correo),
                         new Claim(ClaimTypes.Role, userData.Rol.Tipo),
                        new Claim(ClaimTypes.GivenName, userData.Correo),



                    }));
            }
            else
            {
                await _sessionStorage.DeleteAsync("UserToken");
                claimsPrincipal = _anonymus;
            }
            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(claimsPrincipal)));
        }


        
    }
}
