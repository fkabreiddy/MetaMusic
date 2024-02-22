
using MetaMusic.Data.Context;
using MetaMusic.Data.Entities;
using MetaMusic.Data.OtherEntities;
using MetaMusic.Data.Request;
using MetaMusic.Data.Responses;
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

        private readonly NavigationManager navManager;

        public UserService(ICurrentUser currentUser,NavigationManager navManager, IGoogleAuthService currentUserServices, IMetaMusicDbContext dbContext)
        {
            this.currentUserServices = currentUserServices;
            this.dbContext = dbContext;
            this.currentUser = currentUser;
            this.navManager = navManager;
        }
        public async Task<Result<UsuarioResponse>> Login(UsuarioRequest request)
        {
            try
            {
                var user = await dbContext.Usuarios.Include(u => u.Rol).FirstOrDefaultAsync(u => u.Correo == request.Correo);

                if(user is not null)
                {
                   
                    return new Result<UsuarioResponse>()
                    {
                        Data = user.ToResponse(),
                        Message = "Logeo Exitoso",
                        Success = true
                    };
                }
                    

               
                    var creacion = await Crear(request);

                    if (creacion.Success && creacion.Data is not null)
                    {

                     
                       
                            return new Result<UsuarioResponse>()
                            {
                                Data = creacion.Data,
                                Message = "Creacion Exitoso",
                                Success = true
                            };

                    }
                    else
                    {
                        
                        return new Result<UsuarioResponse>()
                        {
                               
                                Message = "Error en la creacion del usuario",
                                Success = false
                        };
                        
                    }

               





                

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

               
                

                navManager.NavigateTo("/google-log-out", true);

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

        public async Task<Result<UsuarioResponse>> Crear(UsuarioRequest request)
        {
            try
            {
                var usuario = await dbContext.Usuarios.FirstOrDefaultAsync(u => u.Correo == request.Correo);

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

                await dbContext.Usuarios.AddAsync(Usuario.Crear(request));

                await dbContext.SaveChangesAsync();


                var usuarioresponse = await dbContext.Usuarios.FirstOrDefaultAsync(u => u.Correo == request.Correo);

                if (usuarioresponse is null) //problema creando el usuario
                    return new Result<UsuarioResponse>()
                    {

                        Success = false,
                        Message = "Tuvimos un porblema creando tu usuario, intenta mas tarde"
                    };


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
