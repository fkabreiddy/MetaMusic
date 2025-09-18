using MetaMusic.Data.Context;
using MetaMusic.Data.Entities;
using MetaMusic.Data.OtherEntities;
using MetaMusic.Data.Request;
using MetaMusic.Data.Responses;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.EntityFrameworkCore;
using System;
using System.Security.Claims;

namespace MetaMusic.Data.Services
{
    public class GoogleAuthService : IGoogleAuthService
    {

        private readonly IHttpContextAccessor Context;

        private readonly NavigationManager navigationManager;

        private readonly IMetaMusicDbContext dbContext;





        public GoogleAuthService(IMetaMusicDbContext dbContext, IHttpContextAccessor Context, NavigationManager navigationManager)
        {
            this.dbContext = dbContext;
            this.Context = Context;
            this.navigationManager = navigationManager;



        }


        public async Task<Result<LoginResponse>> GetCurrentUser()
        {
            try
            {



                var email = Context.HttpContext?.User?.FindFirst(ClaimTypes.Email)?.Value;



                if (email is null)
                    return new Result<LoginResponse>()
                    {
                        Message = "No estas logeado",
                        Success = false
                    };


                var user = await dbContext.Usuarios.FirstOrDefaultAsync(u => u.Correo == email);

                if (user is null)
                    return new Result<LoginResponse>()
                    {
                        Message = "No estas registrado",
                        Success = false
                    };


                return new Result<LoginResponse>()
                {
                    Message = "Bienvenido",
                    Data = user.ToLoginResponse(),
                    Success = true
                };
            }
            catch (Exception e)
            {
                return new Result<LoginResponse>()
                {
                    Message = e.InnerException?.Message ?? e.Message,

                    Success = false
                };
            }
        }

        


    }

}
