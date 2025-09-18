using MetaMusic.Data.Context;
using MetaMusic.Data.Entities;
using MetaMusic.Data.OtherEntities;
using MetaMusic.Data.Request;
using MetaMusic.Data.Responses;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

namespace MetaMusic.Data.Services
{
    public class BorradorService : IBorradorService
    {
        private readonly IMetaMusicDbContext dbContext;
        private readonly IGoogleAuthService googleAuth;
        public BorradorService(IMetaMusicDbContext dbContext,
                           IGoogleAuthService googleAuth)
        {
            this.dbContext = dbContext;
            this.googleAuth = googleAuth;
        }

        public async Task<Result<AlbumResponse>> CrearBorrador(AlbumRequest request, CalificacionRequest calificacion, ReviewRequest review)
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


                if(userUser.Rol is not null)
                {
                    if (userUser.Rol.Tipo != "Staff")
                        return new Result<AlbumResponse>()
                        {
                            Success = false,
                            Message = "No tienes permiso para crear esto"
                        };
                }
                else
                {
                    return new Result<AlbumResponse>()
                    {
                        Success = false,
                        Message = "No tienes permiso para crear esto"
                    };
                }
               
              



                calificacion.Usuario = userUser;
                request.Calificaciones.Add(Calificacion.Crear(calificacion));
                review.Creador = userUser;
                request.Review = Review.Crear(review);
                request.Creador = userUser;
                request.Publicado = false;
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
        public async Task<Result<AlbumResponse>> Modificar(AlbumRequest request, CalificacionRequest calificacion, ReviewRequest review)
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

                if (userUser.Rol is not null)
                {
                    if (userUser.Rol.Tipo != "Staff")
                        return new Result<AlbumResponse>()
                        {
                            Success = false,
                            Message = "No tienes permiso para crear esto"
                        };
                }
                else
                {
                    return new Result<AlbumResponse>()
                    {
                        Success = false,
                        Message = "No tienes permiso para crear esto"
                    };
                }


                var album = await dbContext.Albumes.FirstOrDefaultAsync(a => a.Id == request.Id);


                if (album is null)
                    return new() { Message = "Album no encontrado", Success = false };
                    album.Modificar(request);

                var ca = await dbContext.Calificaciones.FirstOrDefaultAsync(c => c.Id == calificacion.Id);


                if (ca is not null)

                    ca.Modificar(calificacion);

                var re = await dbContext.Reviews.FirstOrDefaultAsync(r => r.Id == review.Id);

                if (re is not null)
                    re.Modificar(review);





                await dbContext.SaveChangesAsync();

                return new Result<AlbumResponse>()
                {
                    Data = album.ToResponse(),
                    Message = "Modificacion Correcta",
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
        public async Task<Result<bool>> Eliminar(AlbumResponse response)
        {
            try
            {
                var album1 = await dbContext.Albumes.FirstOrDefaultAsync(a => a.Id == response.Id);

                if (album1 is null)
                    return new Result<bool>()
                    { Message = "Borrador no encotrado", Success = false };




                var calificaciones = await dbContext.Calificaciones.Where(n => n.Album!.Id == response.Id).ToListAsync();
                if (calificaciones is not null)
                {
                    dbContext.Calificaciones.RemoveRange(calificaciones);

                }


                dbContext.Albumes.Remove(album1);
                await dbContext.SaveChangesAsync();
                return new Result<bool>() { Message = "Success", Success = true, Data = true };

            }
            catch (Exception e)
            {

                return new Result<bool>()
                { Message = e.InnerException?.Message ?? e.Message, Success = false };
            }
        }
        public async Task<Result<List<AlbumResponse>>> ConsultarMisBorradoes(UsuarioResponse user)
        {
            try
            {
                var albumes = await dbContext.Albumes.Include(a => a.Review).Include(a => a.Creador).Include(a => a.Artistas).ThenInclude(x => x.Artista!).ThenInclude(a => a.GenerosMusicales).ThenInclude(g => g.Genero).Where(a => a.Creador!.Id == user.Id && a.Publicado == true).ToListAsync();

                if (albumes is null)
                    return new Result<List<AlbumResponse>>()
                    { Message = "Album no encotrado", Success = false };


                return new Result<List<AlbumResponse>>() { Message = "Success", Success = true, Data = albumes.Select(a => a.ToResponse()).ToList() };

            }
            catch (Exception e)
            {

                return new Result<List<AlbumResponse>>()
                { Message = e.InnerException?.Message ?? e.Message, Success = false };
            }
        }
    }
}
