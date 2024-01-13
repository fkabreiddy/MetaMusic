using MetaMusic.Data.Context;
using MetaMusic.Data.Entities;
using MetaMusic.Data.OtherEntities;
using MetaMusic.Data.Request;
using MetaMusic.Data.Responses;
using Microsoft.EntityFrameworkCore;

namespace MetaMusic.Data.Services
{
    public class AlbumService : IAlbumService
    {
        private readonly IMetaMusicDbContext dbContext;
        private readonly IGoogleAuthService googleAuth;
        public AlbumService(IMetaMusicDbContext dbContext,
                           IGoogleAuthService googleAuth)
        {
            this.dbContext = dbContext;
            this.googleAuth = googleAuth;
        }

        public async Task<Result<AlbumResponse>> Crear(AlbumRequest request, CalificacionRequest calificacion, ReviewRequest review)
        {
            try
            {
                var currenUserData = await googleAuth.GetCurrentUser();


                if (currenUserData.Data is null)
                    return new Result<AlbumResponse>()
                    {
                        Success = false,
                        Message = "No estas logeado"
                    };


                var userUser = await dbContext.Usuarios.Include(u => u.Rol).FirstOrDefaultAsync(u => u.Id == currenUserData.Data.Id);

                if (userUser is null)
                    return new Result<AlbumResponse>()
                    {
                        Success = false,
                        Message = "No existes en nuestros registros"
                    };

                if (userUser.Rol.Tipo != "Staff")
                    return new Result<AlbumResponse>()
                    {
                        Success = false,
                        Message = "No tienes permiso para crear esto"
                    };
                calificacion.Usuario = userUser;
                request.Calificaciones.Add(Calificacion.Crear(calificacion));
                review.Creador = userUser;
                request.Review = Review.Crear(review);

                request.Creador = userUser;

                request.Fecha_Agregado = DateTime.Now;

                var newalbum = Album.Crear(request);
                await dbContext.Albumes.AddAsync(newalbum);


                await dbContext.SaveChangesAsync();

                return new Result<AlbumResponse>()
                {
                    Data = newalbum.ToResponse(),
                    Message = "Creacion Correcta",
                    Success = true

                };
            }
            catch (Exception e)
            {
                return new Result<AlbumResponse>()
                {

                    Message = e.InnerException?.Message ?? e.Message,
                    Success = true

                };
            }
        }
    }
}
