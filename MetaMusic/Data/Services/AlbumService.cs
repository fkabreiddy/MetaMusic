using MetaMusic.Data.Context;
using MetaMusic.Data.Entities;
using MetaMusic.Data.OtherEntities;
using MetaMusic.Data.Request;
using MetaMusic.Data.Responses;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Globalization;

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
                request.Fecha_Publicacion_Formateada = DateTime.ParseExact(request.Fecha_Publicacion, "yyyy-MM-dd", CultureInfo.InvariantCulture);
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

        public async Task<Result<AlbumResponse>> ConsultarUno(string spotifyId)
        {
            try
            {
                var album1 = await dbContext.Albumes.FirstOrDefaultAsync(a => a.IdSpotify == a.IdSpotify);

                if (album1 is null)
                    return new Result<AlbumResponse>()
                    { Message = "Album no encotrado", Success = false };

                var album = await dbContext.Albumes.Include(a => a.Calificaciones).ThenInclude(c => c.Usuario).Include(a => a.Review).Include(a => a.Tracks).Include(a => a.Creador).Include(a => a.Artistas).ThenInclude(x => x.Artista).ThenInclude(a => a.GenerosMusicales).ThenInclude(g => g.Genero).FirstOrDefaultAsync(a => a.Id == album1.Id);

                return new Result<AlbumResponse>() { Message = "Success", Success = true, Data = album.ToResponse() };

            }
            catch (Exception e)
            {

                return new Result<AlbumResponse>()
                { Message = e.InnerException?.Message ?? e.Message, Success = false };
            }
        }
        public async Task<Result<Track>> LikeTrack(Track track)
        {
            try
            {
                var currentuser = await googleAuth.GetCurrentUser();

                if (currentuser is null)
                    return new Result<Track>()
                    { Message = "No estas logeado", Success = false };

                var tr = await dbContext.Tracks.FirstOrDefaultAsync(a => a.Id == track.Id);
                var user = await dbContext.Usuarios.FirstOrDefaultAsync(u => u.Id == currentuser.Data.Id);

                if (currentuser is null)
                    return new Result<Track>()
                    { Message = "No estas logeado", Success = false };

                var interaccion = new Usuario_Like_Track() { Track = tr, Usuario = user };

                await dbContext.Usuario_Like_Tracks.AddAsync(interaccion);
                await dbContext.SaveChangesAsync();

                var trackadevolver = await dbContext.Tracks.Include(t => t.Usuarios_Liked).ThenInclude(x =>x.Usuario).FirstOrDefaultAsync(t => t.Id == track.Id);

                return new Result<Track>() { Message = "Success", Success = true, Data = trackadevolver };

            }
            catch (Exception e)
            {

                return new Result<Track>()
                { Message = e.InnerException?.Message ?? e.Message, Success = false };
            }
        }

        public async Task<Result<Track>> DisLikeTrack(Track track)
        {
            try
            {
                var currentuser = await googleAuth.GetCurrentUser();

                if (currentuser is null)
                    return new Result<Track>()
                    { Message = "No estas logeado", Success = false };


                var user = await dbContext.Usuarios.FirstOrDefaultAsync(u => u.Id == currentuser.Data.Id);

                if (currentuser is null)
                    return new Result<Track>()
                    { Message = "No estas logeado", Success = false };

                var interaccion = await dbContext.Usuario_Like_Tracks.FirstOrDefaultAsync(u => u.Usuario.Id == user.Id && u.Track.Id == track.Id);


                if (interaccion is null)
                    return new Result<Track>()
                    { Message = "Error", Success = false };

                dbContext.Usuario_Like_Tracks.Remove(interaccion);
                await dbContext.SaveChangesAsync();

                var trackadevolver = await dbContext.Tracks.Include(t => t.Usuarios_Liked).ThenInclude(x => x.Usuario).FirstOrDefaultAsync(t => t.Id == track.Id);

                return new Result<Track>() { Message = "Success", Success = true, Data = trackadevolver };

            }
            catch (Exception e)
            {

                return new Result<Track>()
                { Message = e.InnerException?.Message ?? e.Message, Success = false };
            }
        }


        public async Task<Result<List<Track>>> GetTrackData(AlbumResponse album)
        {
            try
            {


                var tracks = await dbContext.Tracks.Include(r => r.Usuarios_Liked).ThenInclude(r => r.Usuario).Where(u => u.Album.Id == album.Id).ToListAsync();


                if (tracks is null)
                    return new Result<List<Track>>()
                    { Message = "Error", Success = false };


                return new Result<List<Track>>() { Message = "Success", Success = true, Data = tracks.ToList() };

            }
            catch (Exception e)
            {

                return new Result<List<Track>>()
                { Message = e.InnerException?.Message ?? e.Message, Success = false };
            }
        }

        //public async Task<Result<List<AlbumResponse>>> Consultar(string filtro)
        //{

        //}

        //public async Task<Result<List<AlbumResponse>>> ConsultarPorGenero(GeneroResponse Genero)
        //{

        //}

        //public async Task<Result<List<AlbumResponse>>> ConsultarTodos(GeneroResponse Genero)
        //{

        //}
    }
}
