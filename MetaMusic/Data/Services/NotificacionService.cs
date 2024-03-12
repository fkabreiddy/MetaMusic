using MetaMusic.Data.Context;
using MetaMusic.Data.Entities;
using MetaMusic.Data.OtherEntities;
using MetaMusic.Data.Request;
using MetaMusic.Data.Responses;
using Microsoft.EntityFrameworkCore;

namespace MetaMusic.Data.Services
{
    public class NotificacionService : INotificacionService
    {

        private readonly IMetaMusicDbContext dbContext;
        private readonly IGoogleAuthService currentUser;

        public NotificacionService(IMetaMusicDbContext dbContext, IGoogleAuthService currentUser)
        {
            this.dbContext = dbContext;
            this.currentUser = currentUser;
        }


        public async Task<Result<bool>> NotificarNuevoAlbum(int _albumid)
        {
            try
            {
                var usuarioactual = await currentUser.GetCurrentUser();

                if (!usuarioactual.Success)
                    return new()
                    {
                        Message = "No estas logeado",
                        Success = false
                    };

                if (usuarioactual.Data is null)
                    return new() { Message = "Error, no estas logeado", Success = false };

                var userfrom = await dbContext.Usuarios.FirstOrDefaultAsync(u => u.Id == usuarioactual.Data.Id);


                if (userfrom is null)
                    return new()
                    {
                        Message = "No estas logeado",
                        Success = false
                    };

                var album = await dbContext.Albumes.Include(a => a.Artistas).ThenInclude(r => r.Artista!).ThenInclude(a => a.Suscriptores).ThenInclude(r => r.Usuario).FirstOrDefaultAsync(a => a.Id == _albumid);


                if (album is null)
                    return new Result<bool>()
                    {
                        Message = "Error Inesperado intente mas luego",
                        Success = false
                    };


                var suscriptores = album.Artistas
                                                .SelectMany(r => r.Artista!.Suscriptores.Select(a => a.Usuario))
                                                .Distinct()
                                                .ToList();



                if (suscriptores is not null && suscriptores.Count() >= 1)
                {
                    foreach (var suscriptor in suscriptores)
                    {
                        var notificacion = Notificacion.Crear(new NotificacionRequest() { Album = album, Titulo = "Nueva review para ti", UserFrom = userfrom, UserTo = suscriptor });


                        await dbContext.Notificacions.AddAsync(notificacion);
                    }
                }


                await dbContext.SaveChangesAsync();

                return new Result<bool>()
                {
                    Message = "Exito",
                    Success = true
                };
            }
            catch (Exception e)
            {
                return new Result<bool>()
                {
                    Message = e.InnerException?.Message ?? e.Message,
                    Success = false
                };
            }
        }

        public async Task<Result<bool>> NotificarReviewOculta(int _albumid)
        {
            try
            {
                var usuarioactual = await currentUser.GetCurrentUser();

                if (!usuarioactual.Success)
                    return new()
                    {
                        Message = "No estas logeado",
                        Success = false
                    };

                if (usuarioactual.Data is null)
                    return new() { Message = "Error, no estas logeado", Success = false };
                var userfrom = await dbContext.Usuarios.FirstOrDefaultAsync(u => u.Id == usuarioactual.Data.Id);


                if (userfrom is null)
                    return new()
                    {
                        Message = "No estas logeado",
                        Success = false
                    };

                var album = await dbContext.Albumes.Include(a => a.Creador).FirstOrDefaultAsync(a => a.Id == _albumid);


                if (album is null)
                    return new Result<bool>()
                    {
                        Message = "Error Inesperado intente mas luego",
                        Success = false
                    };



                var notificacion = Notificacion.Crear(new NotificacionRequest() { Album = album, Titulo = $"Ocultamos la review del album {album.Nombre} por tener demaciados reportes sin revisar", UserFrom = userfrom, UserTo = album.Creador ?? new Usuario() }); ;


                await dbContext.Notificacions.AddAsync(notificacion);
                


                await dbContext.SaveChangesAsync();

                return new Result<bool>()
                {
                    Message = "Exito",
                    Success = true
                };
            }
            catch (Exception e)
            {
                return new Result<bool>()
                {
                    Message = e.InnerException?.Message ?? e.Message,
                    Success = false
                };
            }
        }

        public async Task<Result<bool>> NotificacionGenerica(int _usertoId, string title)
        {
            try
            {
                var userto = await dbContext.Usuarios.FirstOrDefaultAsync(u => u.Id == _usertoId);


                if (userto is null)
                    return new()
                    {
                        Message = "El usuario que reporto no existe ya",
                        Success = false
                    };


                var usuarioactual = await currentUser.GetCurrentUser();

                if (!usuarioactual.Success)
                    return new()
                    {
                        Message = "No estas logeado",
                        Success = false
                    };
                if (usuarioactual.Data is null)
                    return new() { Message = "Error, no estas logeado", Success = false };

                var userfrom = await dbContext.Usuarios.FirstOrDefaultAsync(u => u.Id == usuarioactual.Data.Id);


                if (userfrom is null)
                    return new()
                    {
                        Message = "No estas logeado",
                        Success = false
                    };

                await dbContext.Notificacions.AddAsync(Notificacion.Crear(new NotificacionRequest() { Album = null, Titulo = title, UserFrom = userfrom, UserTo = userto }));

                await dbContext.SaveChangesAsync();

                return new()
                {
                    Message = "Exito",
                    Success = true
                };
            }
            catch (Exception e)
            {
                return new Result<bool>()
                {
                    Message = e.InnerException?.Message ?? e.Message,
                    Success = false
                };

            }
        }

        public async Task<Result<List<NotificacionResponse>>> Consultar()
        {
            try
            {
                var usuarioactual = await currentUser.GetCurrentUser();

                if (!usuarioactual.Success && usuarioactual.Data is not null)
                    return new()
                    {
                        Message = "No estas logeado",
                        Success = false
                    };
                if (usuarioactual.Data is null)
                    return new() { Message = "Error, no estas logeado", Success = false };

                var notificaciones = await dbContext.Notificacions.Include(n => n.Album).Where(n => n.UserTo.Id == usuarioactual.Data.Id).ToListAsync();

                return new()
                {
                    Message = "Exito",
                    Success = true,
                    Data = notificaciones.Select(n => n.ToResponse()).ToList()
                };
            }
            catch (Exception e)
            {
                return new()
                {
                    Message = e.InnerException?.Message ?? e.Message,
                    Success = false
                };

            }
        }


        public async Task<Result<bool>> Eliminar(int notificationId)
        {

            try
            {
                var r = await dbContext.Notificacions.FirstOrDefaultAsync(n => n.Id == notificationId);

                if (r is null)
                    return new()
                    {
                        Message = "Hubo un problema borrando la notificacion",
                        Success = false


                    };

                dbContext.Notificacions.Remove(r);
                await dbContext.SaveChangesAsync();

                return new()
                {
                    Message = "Notificacion Eliminada",
                    Success = true,

                };
            }
            catch (Exception e)
            {
                return new()
                {
                    Message = e.InnerException?.Message ?? e.Message,
                    Success = false
                };

            }
        }

        public async Task<Result<bool>> MarcarComoLeida(List<int> notificacions)
        {

            try
            {


                if (notificacions is null || notificacions.Count() <= 0)
                    return new()
                    {
                        Message = "No hay notificiones nuevas",
                        Success = false
                    };

               

                foreach(var id in notificacions)
                {
                    var n = await dbContext.Notificacions.FirstOrDefaultAsync(n => n.Id == id);


                    if(n is not null)
                          n.Saw = true;

                }

                await dbContext.SaveChangesAsync();

                return new()
                {
                    Message = "Notificacion Eliminada",
                    Success = true,

                };
            }
            catch (Exception e)
            {
                return new()
                {
                    Message = e.InnerException?.Message ?? e.Message,
                    Success = false
                };

            }
        }




    }
}
