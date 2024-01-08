﻿using MetaMusic.Authentication;
using MetaMusic.Data.Context;
using MetaMusic.Data.Entities;
using MetaMusic.Data.OtherEntities;
using MetaMusic.Data.Request;
using MetaMusic.Data.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore;

namespace MetaMusic.Data.Services
{
    public class UserService : IUserService
    {

        private readonly IGoogleAuthService currentUserServices;
        private readonly IMetaMusicDbContext dbContext;
        private readonly ICustomAuthenticationStateProvider customAuthState;
        private readonly NavigationManager navManager;

        public UserService(NavigationManager navManager, ICustomAuthenticationStateProvider customAuthState, IGoogleAuthService currentUserServices, IMetaMusicDbContext dbContext)
        {
            this.currentUserServices = currentUserServices;
            this.dbContext = dbContext;
            this.customAuthState = customAuthState;
            this.navManager = navManager;
        }
        public async Task<Result<UsuarioResponse>> Login(string email)
        {
            try
            {
                var user = await dbContext.Usuarios.FirstOrDefaultAsync(u => u.Correo == email);



                if (user is null)
                    return new Result<UsuarioResponse>()
                    {
                        Message = "El usuario No existe",
                        Success = false,

                    };


                await customAuthState.UpdateAuthenticationState(user.ToLoginResponse());
                return new Result<UsuarioResponse>()
                {
                    Data = user.ToResponse(),
                    Message = "Logeo Exitoso",
                    Success = true
                };

            }
            catch (Exception e)
            {
                return new Result<UsuarioResponse>()
                {

                    Message = (e is null) ? "Error desconocido" : e.InnerException?.Message ?? e.Message,
                    Success = false
                };
            }


        }
        public async Task<Result> Logout()
        {
            try
            {

                await customAuthState.UpdateAuthenticationState(null!);
                navManager.NavigateTo("Pages/Identity/Logout", true);

                return new Result()
                {

                    Message = "DesLogeo Exitoso",
                    Success = true
                };
            }
            catch (Exception E)
            {
                return new Result()
                {

                    Message = E.InnerException?.Message ?? E.Message,
                    Success = false
                };
            }
        }

        public async Task<Result<UsuarioResponse>> Crear(string email, string fotoperfil, string nombre)
        {
            try
            {
                var usuario = await dbContext.Usuarios.FirstOrDefaultAsync(u => u.Correo == email);

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

                await dbContext.Usuarios.AddAsync(Usuario.Crear(new UsuarioRequest() { Biografia = $"Mi nombre es {nombre}", Correo = email, Nombre = nombre, FotoDePerfil = fotoperfil, Rol = rol }));

                await dbContext.SaveChangesAsync();


                var usuarioresponse = await dbContext.Usuarios.FirstOrDefaultAsync(u => u.Correo == email);

                if (usuarioresponse is null) //problema creando el usuario
                    return new Result<UsuarioResponse>()
                    {

                        Success = false,
                        Message = "Tuvimos un porblema creando tu usuario, intenta mas tarde"
                    };

                await Login(email);
                return new Result<UsuarioResponse>()
                {
                    Data = usuarioresponse.ToResponse(),
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
                var usuarioexistente = await dbContext.Usuarios.FirstOrDefaultAsync(u => u.Correo == email);

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
    }
}