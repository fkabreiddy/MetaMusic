
using MetaMusic.Data.Context;
using MetaMusic.Data.Entities;
using MetaMusic.Data.OtherEntities;
using MetaMusic.Data.Request;
using MetaMusic.Data.Responses;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using static MudBlazor.CategoryTypes;

namespace MetaMusic.Data.Services
{
    public class UserService : IUserService
    {

        private readonly IGoogleAuthService currentUserServices;
        private readonly IMetaMusicDbContext dbContext;
        private readonly ICurrentUser currentUser;
        private readonly IHttpContextAccessor http;

        private readonly NavigationManager navManager;

        public UserService(IHttpContextAccessor http,ICurrentUser currentUser, NavigationManager navManager, IGoogleAuthService currentUserServices, IMetaMusicDbContext dbContext)
        {
            this.currentUserServices = currentUserServices;
            this.dbContext = dbContext;
            this.currentUser = currentUser;
            this.navManager = navManager;
            this.http = http;
        }
        public async Task<Result<LoginResponse>> Login(string email)
        {
            try
            {
                var user = await dbContext.Usuarios.Include(u => u.Rol).FirstOrDefaultAsync(u => u.Correo == email);

                if (user is null)
                {

                    return new()
                    {

                        Message = "Error, el Usuario No esta registrado",
                        Success = false
                    };
                }

                return new()
                {
                    Data = user.ToLoginResponse(),
                    Message = "Logeo Exitoso",
                    Success = true
                };




            }
            catch (Exception e)
            {
                return new()
                {

                    Message = (e is null) ? "Error desconocido" : e.InnerException?.Message ?? e.Message,
                    Success = false
                };
            }


        }

        
        public async Task<Result<bool>> Logout()
        {
            try
            {




                var authenticationProperties = new AuthenticationProperties
                {
                    AllowRefresh = false,
                    IsPersistent = true,
                };

                await http.HttpContext!.SignOutAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme, authenticationProperties);

                http.HttpContext!.Session.Clear();

                return new ()
                {

                    Message = "DesLogeo Exitoso",
                    Success = true,
                    Data = true
                };
            }
            catch (Exception E)
            {
                return new ()
                {

                    Message = E.InnerException?.Message ?? E.Message,
                    Success = false
                };
            }
        }

        public async Task<Result<UsuarioResponse>> Crear(string nombre, string correo, string avatar)
        {
            try
            {
                var usuario = await dbContext.Usuarios.FirstOrDefaultAsync(u => u.Correo == correo);

                if (usuario is not null) //si existe el usuario 
                    return new Result<UsuarioResponse>()
                    {

                        Success = false,
                        Message = "Ya existe una cuenta con este correo"
                    };

                var rol = await dbContext.Roles.FirstOrDefaultAsync(r => r.Tipo == "Normal");

                if (rol is null)
                    return new Result<UsuarioResponse>()
                    {

                        Success = false,
                        Message = "Hubo un problema a la hora de crear tu cuenta, intenta mas tarde"
                    };


            
                UsuarioRequest request = new UsuarioRequest() { Nombre = nombre, Correo = correo, FotoDePerfil = avatar ?? "https://i.pinimg.com/736x/fe/15/9f/fe159f0fa49b03c2907c0826de7ef291.jpg", Biografia = "Hablanos de ti", Rol = rol };
                var newuser = Usuario.Crear(request);
                await dbContext.Usuarios.AddAsync(newuser);

                await dbContext.SaveChangesAsync();





                return new Result<UsuarioResponse>()
                {
                    Data = newuser.ToResponse(),
                    Success = true,
                    Message = "Logeo Exitoso"
                };

            }
            catch (Exception e)
            {

                return new Result<UsuarioResponse>()
                {

                    Success = false,
                    Message = e.InnerException?.Message ?? e.Message
                };
            }
        }

        [Authorize(Roles = "Staff")]
        public async Task<Result<bool>> Eliminar(UsuarioRequest request)
        {
            try
            {

                var currentuserRole = await currentUserServices.GetCurrentUser();
                if (currentuserRole.Data is null || currentuserRole.Data.Rol is null)
                    return new() { Message = "No estas logeado", Success = false };

                if (currentuserRole.Data.Rol.Tipo == "Normal")
                    return new Result<bool>()
                    {

                        Success = false,
                        Message = "No estas autorizado para esta accion"
                    };

                var usuario = await dbContext.Usuarios.FirstOrDefaultAsync(u => u.Correo == request.Correo);

                if (usuario is null) //si existe el usuario 
                    return new Result<bool>()
                    {

                        Success = false,
                        Message = "Este Usuario No Existe"
                    };




                dbContext.Usuarios.Remove(usuario);

                await dbContext.SaveChangesAsync();



                return new Result<bool>()
                {
                    Data = true,
                    Success = false,
                    Message = $"Se elimino el usuario {request.Correo} Correctamente"
                };

            }
            catch (Exception e)
            {

                return new Result<bool>()
                {

                    Success = false,
                    Message = e.InnerException?.Message ?? e.Message
                };
            }
        }


        public async Task<Result<UsuarioResponse>> Modificar(UsuarioRequest request)
        {
            try
            {
                var currentuser = await currentUserServices.GetCurrentUser();

                if (currentuser.Data is null)
                    return new Result<UsuarioResponse>()
                    {

                        Success = false,
                        Message = "No estas Logeado"
                    };

                var usuario = await dbContext.Usuarios.FirstOrDefaultAsync(u => u.Id == currentuser.Data.Id);

                if (usuario is null) //si no existe el usuario 
                    return new Result<UsuarioResponse>()
                    {

                        Success = false,
                        Message = "No estas Logeado"
                    };

                usuario.Modificar(request);


                await dbContext.SaveChangesAsync();




                return new Result<UsuarioResponse>()
                {
                    Data = usuario.ToResponse(),
                    Success = true,
                    Message = "Cambios hechos"
                };

            }
            catch (Exception e)
            {

                return new Result<UsuarioResponse>()
                {

                    Success = false,
                    Message = e.InnerException?.Message ?? e.Message
                };
            }
        }

        public async Task<Result<UsuarioResponse>> ConsultarUsuarioActual()
        {
            try
            {
                var us = await currentUserServices.GetCurrentUser();

                if (us.Data is null)
                    return new Result<UsuarioResponse>
                    {

                        Message = "No estas Logeado",
                        Success = true
                    };
                var usuarioactual = await dbContext.Usuarios.Include(u => u.Rol).FirstOrDefaultAsync(u => u.Id == us.Data.Id);

                if (usuarioactual is null)
                    return new Result<UsuarioResponse>
                    {
                        Message = "No estas Registrado",
                        Success = false
                    };

                return new Result<UsuarioResponse>
                {
                    Data = usuarioactual.ToResponse(),
                    Message = "UsuarioEncontrado",
                    Success = true
                };
            }
            catch (Exception e)
            {

                return new Result<UsuarioResponse>
                {
                    Message = e.InnerException?.Message ?? e.Message,
                    Success = false
                };

            }
        }
        public async Task<Result<UsuarioResponse>> ConsultarUsuario(string email)
        {
            try
            {
                var usuarioexistente = await dbContext.Usuarios.FirstOrDefaultAsync(u => u.CorreoNormalizado == email);



                if (usuarioexistente is null)
                    return new Result<UsuarioResponse>
                    {
                        Message = "Usuario no encontrado",
                        Success = false
                    };


                var usuario = await dbContext.Usuarios.Include(u => u.Rol).FirstOrDefaultAsync(u => u.Id == usuarioexistente.Id);


                if (usuario is null)
                    return new Result<UsuarioResponse>
                    {
                        Message = "Usuario no encontrado",
                        Success = false
                    };

                return new Result<UsuarioResponse>
                {
                    Data = usuario.ToResponse(),
                    Message = "UsuarioEncontrado",
                    Success = true
                };
            }
            catch (Exception e)
            {

                return new Result<UsuarioResponse>
                {
                    Message = e.InnerException?.Message ?? e.Message,
                    Success = false
                };

            }
        }
        public async Task<Result<List<UsuarioResponse>>> BuscarUsuario(string filtro)
        {
            try
            {


                var usuarios = await dbContext.Usuarios.Include(u => u.Rol).Where(u => u.CorreoNormalizado.ToLower() == filtro.ToLower() || u.Nombre.ToLower().Contains(filtro.ToLower())).ToListAsync();


                if (usuarios is null)
                    return new Result<List<UsuarioResponse>>
                    {
                        Message = "Usuario no encontrado",
                        Success = false
                    };

                return new Result<List<UsuarioResponse>>
                {
                    Data = usuarios.Select(u => u.ToResponse()).ToList(),
                    Message = "Usuarios Encontrados",
                    Success = true
                };
            }
            catch (Exception e)
            {

                return new Result<List<UsuarioResponse>>
                {
                    Message = e.InnerException?.Message ?? e.Message,
                    Success = false
                };

            }
        }
    }
}
