using MetaMusic.Data.Context;
using MetaMusic.Data.Entities;
using MetaMusic.Data.OtherEntities;
using MetaMusic.Data.Request;
using MetaMusic.Data.Responses;
using Microsoft.EntityFrameworkCore;
using System.Runtime.InteropServices;

namespace MetaMusic.Data.Services
{
    public class NotaService : INotaService
    {
        private readonly IMetaMusicDbContext dbContext;
        private readonly IGoogleAuthService googleAuth;

        public NotaService(IMetaMusicDbContext dbContext, IGoogleAuthService googleAuth)
        {
            this.dbContext = dbContext;
            this.googleAuth = googleAuth;
        }


        public async Task<Result<NotaResponse>> Crear(NotaRequest request, CalificacionRequest calificacionrequest, AlbumRequest albumrequest)
        {

            try
            {
                var currentUser = await googleAuth.GetCurrentUser();

                if (currentUser.Data is null)
                    return new Result<NotaResponse>() { Message = "No estas logeado", Success = false };

                var usuarioactual = await dbContext.Usuarios.FirstOrDefaultAsync(u => u.Id == currentUser.Data.Id);

                if (usuarioactual is null)
                    return new Result<NotaResponse>() { Message = "Primero crea una cuenta", Success = false };

                var album = await dbContext.Albumes.FirstOrDefaultAsync(a => a.Id == a.Id);

                if (album is null)
                    return new Result<NotaResponse>() { Message = "El album no existe", Success = false };

                calificacionrequest.Usuario = usuarioactual;
                calificacionrequest.Album = album;
                request.Album = album;
                request.Creador = usuarioactual;

                await dbContext.Notas.AddAsync(Nota.Crear(request));
                await dbContext.Calificaciones.AddAsync(Calificacion.Crear(calificacionrequest));

                await dbContext.SaveChangesAsync();

                var notaadevolver = await dbContext.Notas.FirstOrDefaultAsync(n => n.Id == request.Id);

                if (notaadevolver is null)
                    return new Result<NotaResponse>() { Message = "Hubo un error a la hora de crear su nota, los cambios no se guardaron.", Success = false };

                return new Result<NotaResponse>()
                {
                    Data = notaadevolver.ToResponse(),
                    Message = "Nota publicada",
                    Success = true
                };





            }
            catch (Exception e)
            {
                return new Result<NotaResponse>()
                {

                    Message = e.InnerException?.Message ?? e.Message,
                    Success = false
                };
            }
        }
    }
}
