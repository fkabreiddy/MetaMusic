using MetaMusic.Data.Context;
using MetaMusic.Data.Entities;
using MetaMusic.Data.OtherEntities;
using MetaMusic.Data.Request;
using MetaMusic.Data.Responses;
using Microsoft.AspNetCore.Components;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Globalization;
using System.Linq.Dynamic.Core;

namespace MetaMusic.Data.Services
{
    public class AlbumService : IAlbumService
    {
        private readonly IMetaMusicDbContext dbContext;
        private readonly IGoogleAuthService googleAuth;
        private readonly NavigationManager navManager;

        private readonly INotificacionService notificacionesService;
        public AlbumService(IMetaMusicDbContext dbContext,
                           IGoogleAuthService googleAuth, NavigationManager navManager, INotificacionService notificacionesService)
        {
            this.dbContext = dbContext;
            this.googleAuth = googleAuth;
            this.notificacionesService = notificacionesService;
            this.navManager = navManager;
        }

        public async Task<Result<AlbumResponse>> Crear(AlbumRequest request, ReviewRequest review)
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

                if (userUser.Rol?.Tipo != "Staff")
                    return new Result<AlbumResponse>()
                    {
                        Success = false,
                        Message = "No tienes permiso para crear esto"
                    };







                review.Creador = userUser;
                request.Review = Review.Crear(review);

                request.Creador = userUser;



                var newalbum = Album.Crear(request);
                await dbContext.Albumes.AddAsync(newalbum);


                await dbContext.SaveChangesAsync();


                await notificacionesService.NotificarNuevoAlbum(newalbum.Id);
                var r = await dbContext.Peticiones.Include(r => r.Usuario).Where(p => p.AlbumSpotifyId == newalbum.IdSpotify).ToListAsync();

                if (r is not null)
                    dbContext.Peticiones.RemoveRange(r);

                foreach (var peticion in r)
                {
                    await notificacionesService.NotificacionGenerica(peticion.Usuario.Id, $"Por que lo pediste: {newalbum.Nombre}");

                }
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

        public async Task<Result<AlbumResponse>> GetSingleReferenceAlbum(int albumId)
        {
            try
            {
                var album = await dbContext.Albumes.Include(a => a.Artistas).ThenInclude(x => x.Artista).ThenInclude(a => a.GenerosMusicales).ThenInclude(g => g.Genero).FirstOrDefaultAsync(a => a.Id == albumId);

                if (album is null)
                    return new Result<AlbumResponse>()
                    {

                        Message = "Album no encontrado",
                        Success = false

                    };

                return new Result<AlbumResponse>()
                {
                    Data = album.ToResponse(),
                    Message = "Album Encontrado",
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
        public async Task<Result<AlbumResponse>> CrearSingle(AlbumRequest request, ReviewRequest review, bool enlazarAlbum)
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

                if (userUser.Rol?.Tipo != "Staff")
                    return new Result<AlbumResponse>()
                    {
                        Success = false,
                        Message = "No tienes permiso para crear esto"
                    };







                review.Creador = userUser;
                request.Review = Review.Crear(review);

                request.Creador = userUser;
                request.IsSingle = true;

                if (enlazarAlbum)
                {
                    var album = await dbContext.Albumes.FirstOrDefaultAsync(a => a.Tracks.Any(t => t.Titulo == request.Nombre && a.Artistas[0].Artista.Id == request.Artistas[0].Artista.Id &&  a.Id != request.Id));


                    if (album is null)
                    {
                        var otroalbum = await dbContext.Albumes.Where(a => a.Artistas[0].Artista.Id == request.Artistas[0].Artista.Id && a.Id != request.Id).OrderByDescending(a => a.Calificacion_Creador).FirstOrDefaultAsync();

                        if (otroalbum is not null)
                        {
                            request.Reference = otroalbum.Id;
                        }
                    }
                    else
                    {
                        request.Reference = album.Id;
                    }


                }

                var newalbum = Album.Crear(request);
                await dbContext.Albumes.AddAsync(newalbum);


                await dbContext.SaveChangesAsync();


                await notificacionesService.NotificarNuevoAlbum(newalbum.Id);
                var r = await dbContext.Peticiones.Include(r => r.Usuario).Where(p => p.AlbumSpotifyId == newalbum.IdSpotify).ToListAsync();

                if (r is not null)
                    dbContext.Peticiones.RemoveRange(r);

                foreach (var peticion in r)
                {
                    await notificacionesService.NotificacionGenerica(peticion.Usuario.Id, $"Por que lo pediste: {newalbum.Nombre}");

                }
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

        public async Task<Result<AlbumResponse>> Modificar(AlbumRequest request, ReviewRequest review)
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

                if (userUser.Rol?.Tipo != "Staff")
                    return new Result<AlbumResponse>()
                    {
                        Success = false,
                        Message = "No tienes permiso para crear esto"
                    };


                var album = await dbContext.Albumes.FirstOrDefaultAsync(a => a.Id == request.Id);


                if (album is not null)
                    album.Modificar(request);


                var re = await dbContext.Reviews.FirstOrDefaultAsync(r => r.Id == review.Id);

                if (re is not null)
                    re.Modificar(review);





                await dbContext.SaveChangesAsync();

                return new Result<AlbumResponse>()
                {
                    Data = album?.ToResponse(),
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

        public async Task<Result<AlbumResponse>> ModificarSingle(AlbumRequest request, ReviewRequest review, bool enlazarAlbum)
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

                if (userUser.Rol?.Tipo != "Staff")
                    return new Result<AlbumResponse>()
                    {
                        Success = false,
                        Message = "No tienes permiso para crear esto"
                    };


                var album = await dbContext.Albumes.FirstOrDefaultAsync(a => a.Id == request.Id);


                if (enlazarAlbum && request.Reference == 0)
                {
                    var referencia = await dbContext.Albumes.FirstOrDefaultAsync(a => a.Tracks.Any(t => t.Titulo == request.Nombre && a.Artistas[0].Artista.Id == request.Artistas[0].Artista.Id && a.Id != request.Id));

                    if (referencia is null)
                    {
                        var otroalbum = await dbContext.Albumes.Where(a => a.Artistas[0].Artista.Id == request.Artistas[0].Artista.Id && a.Id != request.Id).OrderByDescending(a => a.Calificacion_Creador).FirstOrDefaultAsync();

                        if (otroalbum is not null)
                        {
                            request.Reference = otroalbum.Id;
                        }
                    }
                    else
                    {
                        request.Reference = referencia.Id;
                    }


                }

                if (album is not null)
                    album.Modificar(request);


                var re = await dbContext.Reviews.FirstOrDefaultAsync(r => r.Id == review.Id);

                if (re is not null)
                    re.Modificar(review);





                await dbContext.SaveChangesAsync();

                return new Result<AlbumResponse>()
                {
                    Data = album?.ToResponse(),
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
        public async Task<Result<AlbumResponse>> ConsultarUno(string spotifyId)
        {
            try
            {
                var album1 = await dbContext.Albumes.FirstOrDefaultAsync(a => a.IdSpotify == spotifyId);

                if (album1 is null)
                    return new Result<AlbumResponse>()
                    { Message = "Album no encotrado", Success = false };

                var album = await dbContext.Albumes.Include(a => a.Calificaciones).ThenInclude(c => c.Usuario).Include(a => a.Review).Include(a => a.Tracks).Include(a => a.Creador).Include(a => a.Artistas).ThenInclude(x => x.Artista).ThenInclude(a => a.GenerosMusicales).ThenInclude(g => g.Genero).FirstOrDefaultAsync(a => a.Id == album1.Id && a.Publicado == true && a.IsSingle == false);

                return new Result<AlbumResponse>() { Message = "Success", Success = true, Data = album?.ToResponse() ?? null };

            }
            catch (DbUpdateException ex)
            {
                if (ex.InnerException is SqlException sqlException && NetworkError.IsNetworkError(sqlException.Number))
                {

                    navManager.NavigateTo("network-error");
                    return new Result<AlbumResponse>()
                    { Message = ex.InnerException?.Message ?? ex.Message, Success = false };
                }
                else
                {
                    return new Result<AlbumResponse>()
                    { Message = ex.InnerException?.Message ?? ex.Message, Success = false };
                }

            }
            catch (Exception e)
            {

                return new Result<AlbumResponse>()
                { Message = e.InnerException?.Message ?? e.Message, Success = false };
            }
        }

        public async Task<Result<AlbumResponse>> ConsultarUnSingle(string spotifyId)
        {
            try
            {
                var album1 = await dbContext.Albumes.FirstOrDefaultAsync(a => a.IdSpotify == spotifyId);

                if (album1 is null)
                    return new Result<AlbumResponse>()
                    { Message = "Single no encotrado", Success = false };

                var album = await dbContext.Albumes.Include(a => a.Review).Include(a => a.Creador).Include(a => a.Artistas).ThenInclude(x => x.Artista).ThenInclude(a => a.GenerosMusicales).ThenInclude(g => g.Genero).FirstOrDefaultAsync(a => a.Id == album1.Id && a.Publicado == true);

                if (album is null)
                    return new() { Message = "Single no encontrado" };

                return new Result<AlbumResponse>() { Message = "Success", Success = true, Data = album?.ToResponse() };

            }
            catch (Exception e)
            {

                return new Result<AlbumResponse>()
                { Message = e.InnerException?.Message ?? e.Message, Success = false };
            }
        }

        public async Task<Result<Usuario_Like_Track>> LikeTrack(Track track)
        {
            try
            {
                var currentuser = await googleAuth.GetCurrentUser();

                if (currentuser is null)
                    return new Result<Usuario_Like_Track>()
                    { Message = "No estas logeado", Success = false };

                var tr = await dbContext.Tracks.FirstOrDefaultAsync(a => a.Id == track.Id);
                var user = await dbContext.Usuarios.FirstOrDefaultAsync(u => u.Id == currentuser.Data.Id);

                if (tr is null)
                    return new Result<Usuario_Like_Track>()
                    { Message = "No existe el track", Success = false };

                if (currentuser is null)
                    return new Result<Usuario_Like_Track>()
                    { Message = "No estas logeado", Success = false };

                var interaccion = new Usuario_Like_Track() { Track = tr, Usuario = user };

                await dbContext.Usuario_Like_Tracks.AddAsync(interaccion);

                tr.Cantidad_Likes += 1;
                await dbContext.SaveChangesAsync();


                return new Result<Usuario_Like_Track>() { Message = "Success", Success = true, };

            }
            catch (IOException ex)
            {
                if (ex.InnerException is SqlException sqlException && NetworkError.IsNetworkError(sqlException.Number))
                {

                    navManager.NavigateTo("network-error");
                    return new Result<Usuario_Like_Track>() { Message = "Error de conexion", Success = false, };
                }
                else
                {
                    navManager.NavigateTo("network-error");
                    return new Result<Usuario_Like_Track>() { Message = "Error desconocido", Success = true, };
                }

            }
            catch (Exception e)
            {

                return new Result<Usuario_Like_Track>()
                { Message = e.InnerException?.Message ?? e.Message, Success = false };
            }
        }

        public async Task<Result<Usuario_Like_Track>> DisLikeTrack(Track track)
        {
            try
            {
                var currentuser = await googleAuth.GetCurrentUser();

                if (currentuser is null)
                    return new Result<Usuario_Like_Track>()
                    { Message = "No estas logeado", Success = false };


                var user = await dbContext.Usuarios.FirstOrDefaultAsync(u => u.Id == currentuser.Data.Id);

                if (currentuser is null)
                    return new Result<Usuario_Like_Track>()
                    { Message = "No estas logeado", Success = false };

                var tr = await dbContext.Tracks.FirstOrDefaultAsync(a => a.Id == track.Id);
                if (tr is null)
                    return new Result<Usuario_Like_Track>()
                    { Message = "No existe el track", Success = false };

                var interaccion = await dbContext.Usuario_Like_Tracks.FirstOrDefaultAsync(u => u.Usuario.Id == user.Id && u.Track.Id == track.Id);


                if (interaccion is null)
                    return new Result<Usuario_Like_Track>()
                    { Message = "Error", Success = false };

                dbContext.Usuario_Like_Tracks.Remove(interaccion);
                tr.Cantidad_Likes -= 1;
                await dbContext.SaveChangesAsync();


                return new Result<Usuario_Like_Track>() { Message = "Success", Success = true };

            }
            catch (Exception e)
            {

                return new Result<Usuario_Like_Track>()
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


        public async Task<Result<bool>> Eliminar(AlbumResponse response)
        {
            try
            {
                var album1 = await dbContext.Albumes.FirstOrDefaultAsync(a => a.Id == response.Id);

                if (album1 is null)
                    return new Result<bool>()
                    { Message = "Album no encotrado", Success = false };

                var notas = await dbContext.Notas.Where(n => n.Album.Id == response.Id).ToListAsync();

                if (notas is not null)
                {
                    dbContext.Notas.RemoveRange(notas);

                }

                var calificaciones = await dbContext.Calificaciones.Where(n => n.Album.Id == response.Id).ToListAsync();
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

        public async Task<Result<List<AlbumResponse>>> ConsultarRecientes()
        {
            try
            {
                var albumes = await dbContext.Albumes.Include(a => a.Review).Include(a => a.Tracks).Include(a => a.Creador).Include(a => a.Artistas).ThenInclude(x => x.Artista).ThenInclude(a => a.GenerosMusicales).ThenInclude(g => g.Genero).Where(a => a.Publicado == true && a.IsSingle == false).OrderByDescending(a => a.Fecha_Agregado).Take(3).ToListAsync();

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

        public async Task<Result<List<AlbumResponse>>> ConsultarRecientesSingles()
        {
            try
            {
                var albumes = await dbContext.Albumes.Include(a => a.Review).Include(a => a.Tracks).Include(a => a.Creador).Include(a => a.Artistas).ThenInclude(x => x.Artista).ThenInclude(a => a.GenerosMusicales).ThenInclude(g => g.Genero).Where(a => a.Publicado == true && a.IsSingle == true).OrderByDescending(a => a.Fecha_Agregado).Take(4).ToListAsync();

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
        public async Task<Result<List<AlbumResponse>>> ConsultarVarios(int cantidad)
        {
            try
            {
                var albumes = await dbContext.Albumes.Include(a => a.Calificaciones).ThenInclude(c => c.Usuario).Include(a => a.Review).Include(a => a.Tracks).Include(a => a.Creador).Include(a => a.Artistas).ThenInclude(x => x.Artista).ThenInclude(a => a.GenerosMusicales).ThenInclude(g => g.Genero).Where(a => a.Publicado == true ).OrderByDescending(a => a.Fecha_Agregado).Take(cantidad).ToListAsync();

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

    
        public async Task<Result<List<AlbumResponse>>> BuscarVarios(string filtro)
        {
            try
            {
                var albumes = await dbContext.Albumes.Include(a => a.Calificaciones).ThenInclude(c => c.Usuario).Include(a => a.Review).Include(a => a.Tracks).Include(a => a.Creador).Include(a => a.Artistas).ThenInclude(x => x.Artista).ThenInclude(a => a.GenerosMusicales).ThenInclude(g => g.Genero).Where(a => a.Publicado == true && a.Nombre.ToLower().Contains(filtro.ToLower())).OrderByDescending(a => a.Fecha_Agregado).ToListAsync();

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
        public async Task<Result<List<AlbumResponse>>> GetMore(int startIndex, int cantidad)
        {
            try
            {
                var albumes = await dbContext.Albumes.Include(a => a.Calificaciones).ThenInclude(c => c.Usuario).Include(a => a.Review).Include(a => a.Tracks).Include(a => a.Creador).Include(a => a.Artistas).ThenInclude(x => x.Artista).ThenInclude(a => a.GenerosMusicales).ThenInclude(g => g.Genero).Where(a => a.Publicado == true ).OrderByDescending(a => a.Fecha_Agregado).Skip(startIndex).Take(cantidad).ToListAsync();

                if (albumes is null)
                    return new Result<List<AlbumResponse>>()
                    { Message = "No ha mas albumes", Success = false };


                return new Result<List<AlbumResponse>>() { Message = "Success", Success = true, Data = albumes.Select(a => a.ToResponse()).ToList() };

            }
            catch (Exception e)
            {

                return new Result<List<AlbumResponse>>()
                { Message = e.InnerException?.Message ?? e.Message, Success = false };
            }
        }
        public async Task<Result<List<AlbumResponse>>> ConsultarPorArtista(int artistaid)
        {
            try
            {



                var albumes = await dbContext.Albumes.Include(a => a.Review).Include(a => a.Tracks).ThenInclude(t => t.Usuarios_Liked).ThenInclude(t => t.Usuario).Include(a => a.Creador).Include(a => a.Artistas).ThenInclude(x => x.Artista).ThenInclude(a => a.GenerosMusicales).ThenInclude(g => g.Genero).Where(a => a.Publicado == true && a.Artistas.Any(ar => ar.Artista.Id ==artistaid)).OrderByDescending(a => a.Fecha_Agregado).ToListAsync();

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
        public async Task<Result<List<AlbumResponse>>> ConsultarMisAlbumes(UsuarioResponse user)
        {
            try
            {
                var albumes = await dbContext.Albumes.Include(a => a.Review).Include(a => a.Creador).Include(a => a.Artistas).ThenInclude(x => x.Artista).ThenInclude(a => a.GenerosMusicales).ThenInclude(g => g.Genero).Where(a => a.Creador.Id == user.Id && a.Publicado == true).OrderByDescending(a => a.Fecha_Agregado).ToListAsync();

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

        public async Task<Result<bool>> AlbumOfTheMonthUpdate()
        {
            try
            {


                int previousMonth = DateTime.Now.Month == 1 ? 12 : DateTime.Now.Month - 1;

                var alreadyDone = await dbContext.Albumes.FirstOrDefaultAsync(a => a.Fecha_Agregado.Month == previousMonth && a.IsAlbumOfTheMonth == true && a.IsSingle == true);

                if (alreadyDone is not null)
                    return new Result<bool>()
                    {
                        Message = "Esta tarea ya se hizo",
                        Success = false
                    };

                var albumes = await dbContext.Albumes.Where(a => a.Fecha_Agregado.Month == previousMonth && a.IsAlbumOfTheMonth == false).OrderByDescending(a => a.Calificaciones.Count() * Math.Round(a.Calificaciones.Average(c => c.Numero), 1)).Take(3).ToListAsync();

                if (albumes is not null && albumes.Any())
                {


                    albumes[0].IsAlbumOfTheMonth = true;

                }




                await dbContext.SaveChangesAsync();
                return new Result<bool>()
                {
                    Message = "Exito",
                    Success = true
                };
            }
            catch (Exception ex)
            {
                return new Result<bool>()
                {
                    Message = ex.InnerException?.Message ?? ex.Message,
                    Success = false
                };
            }
        }
        public async Task<Result<AlbumResponse>> GetBestReview()
        {
            try
            {
                int previousMonth = DateTime.Now.Month == 1 ? 12 : DateTime.Now.Month - 1;
                var album = await dbContext.Albumes.Include(a => a.Creador).Include(a => a.Artistas).ThenInclude(a => a.Artista).ThenInclude(a => a.GenerosMusicales).ThenInclude(g => g.Genero).OrderByDescending(a => a.Calificaciones.Count() * Math.Round(a.Calificaciones.Average(c => c.Numero), 1)).FirstOrDefaultAsync(a => a.IsAlbumOfTheMonth == true && a.Fecha_Agregado.Month == previousMonth && a.IsSingle == false);


                if (album == null)
                    album = await dbContext.Albumes.Include(a => a.Creador).Include(a => a.Artistas).ThenInclude(a => a.Artista).ThenInclude(a => a.GenerosMusicales).ThenInclude(g => g.Genero).OrderByDescending(a => a.Calificaciones.Count() * Math.Round(a.Calificaciones.Average(c => c.Numero), 1)).FirstOrDefaultAsync(a => a.IsAlbumOfTheMonth);


                return new Result<AlbumResponse>()
                {
                    Data = album.ToResponse(),
                    Success = true
                };
            }
            catch (Exception e)
            {
                return new()
                {
                    Success = false,
                    Message = e.InnerException?.Message ?? e.Message
                };
            }
        }
    }
}
