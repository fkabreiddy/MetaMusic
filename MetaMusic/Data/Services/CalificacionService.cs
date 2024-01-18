using MetaMusic.Data.Context;
using MetaMusic.Data.Entities;
using MetaMusic.Data.OtherEntities;
using MetaMusic.Data.Request;
using MetaMusic.Data.Responses;
using Microsoft.EntityFrameworkCore;

namespace MetaMusic.Data.Services
{
    public class CalificacionService : ICalificacionService
    {

        private readonly IMetaMusicDbContext dbContext;
        private readonly IGoogleAuthService googleAuthService;

        public CalificacionService(IMetaMusicDbContext dbContext, IGoogleAuthService googleAuthService)
        {
            this.dbContext = dbContext;
            this.googleAuthService = googleAuthService;
        }

        public async Task<Result<CalificacionResponse>> Crear(CalificacionRequest calificacionrequest, AlbumRequest albumrequest)
        {

            try
            {
                var currentUser = await googleAuthService.GetCurrentUser();

                if (currentUser.Data is null)
                    return new Result<CalificacionResponse>() { Message = "No estas logeado", Success = false };

                var usuarioactual = await dbContext.Usuarios.FirstOrDefaultAsync(u => u.Id == currentUser.Data.Id);

                if (usuarioactual is null)
                    return new Result<CalificacionResponse>() { Message = "Primero crea una cuenta", Success = false };

                var album = await dbContext.Albumes.FirstOrDefaultAsync(a => a.Id == a.Id);

                if (album is null)
                    return new Result<CalificacionResponse>() { Message = "El album no existe", Success = false };

                calificacionrequest.Usuario = usuarioactual;
                calificacionrequest.Album = album;



                await dbContext.Calificaciones.AddAsync(Calificacion.Crear(calificacionrequest));

                await dbContext.SaveChangesAsync();

                var calificacion_a_devolver = await dbContext.Calificaciones.FirstOrDefaultAsync(n => n.Id == calificacionrequest.Id);

                if (calificacion_a_devolver is null)
                    return new Result<CalificacionResponse>() { Message = "Hubo un error a la hora de crear su calificacion, los cambios no se guardaron.", Success = false };

                return new Result<CalificacionResponse>()
                {
                    Data = calificacion_a_devolver.ToResponse(),
                    Message = "Nota publicada",
                    Success = true
                };





            }
            catch (Exception e)
            {
                return new Result<CalificacionResponse>()
                {

                    Message = e.InnerException?.Message ?? e.Message,
                    Success = false
                };
            }
        }

        public async Task<Result<List<CalificacionResponse>>> Consultar(AlbumRequest albumrequest)
        {

            try
            {
            

                var album = await dbContext.Albumes.FirstOrDefaultAsync(a => a.Id == a.Id);

                if (album is null)
                    return new Result<List<CalificacionResponse>>() { Message = "El album no existe", Success = false };


                var calificaciones_a_devolver = await dbContext.Calificaciones.Include(c => c.Usuario).Where(c => c.Album.Id == album.Id).ToListAsync();

                if (calificaciones_a_devolver is null)
                    return new Result<List<CalificacionResponse>> () { Message = "Hubo un error a la hora de crear su calificacion, los cambios no se guardaron.", Success = false };

                return new Result<List<CalificacionResponse>>()
                {
                    Data = calificaciones_a_devolver.Select(c => c.ToResponse()).ToList(),
                    Message = "Nota publicada",
                    Success = true
                };





            }
            catch (Exception e)
            {
                return new Result<List<CalificacionResponse>>()
                {

                    Message = e.InnerException?.Message ?? e.Message,
                    Success = false
                };
            }
        }
    }
}
