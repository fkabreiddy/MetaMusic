using MetaMusic.Data.Context;
using MetaMusic.Data.OtherEntities;
using MetaMusic.Data.Entities;
using MetaMusic.Data.Responses;
using SpotifyAPI.Web;
using Microsoft.EntityFrameworkCore;

namespace MetaMusic.Data.Services
{
    public class SpotifyService : ISpotifyService
    {

        private readonly IMetaMusicDbContext dbContext;
        public SpotifyService(IMetaMusicDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<Result<ArtistaResponse>> GetArtista(string artistsId)
        {
            try
            {
                var config = SpotifyClientConfig.CreateDefault();

                var request = new ClientCredentialsRequest("be3b1a5218c449a79a3a21ddfe850a5f", "7cf1d67e3efd4690a1bd43a18903f7dc");
                var response = await new OAuthClient(config).RequestToken(request);

                var spotify = new SpotifyClient(config.WithToken(response.AccessToken));

                var artist = await spotify.Artists.Get(artistsId);
                

                
                ArtistaResponse artistaARetornar = new ArtistaResponse();
                if (artist is null)
                    return new Result<ArtistaResponse>()
                    {
                        Message = "No se encontro el artista",
                        Success = false
                    };


                var existe = await dbContext.Artistas.FirstOrDefaultAsync(a => a.SpotifyId == artist.Id);
                if (existe is not null)
                    return new Result<ArtistaResponse>()
                    {
                        Message = "Este artista ya esta en nuestra plataforma. Intenta con otro",
                        Success = false
                    };

                if (artist.Images.Count() >= 1)
                {
                    artistaARetornar.Foto_Perfil = artist.Images[0].Url;

                }

                artistaARetornar.SpotifyId = artist.Id;
                artistaARetornar.Nombre = artist.Name;

                return new Result<ArtistaResponse>()
                {
                    Data = artistaARetornar,
                    Message = "Si se encontro el artista",
                    Success = true
                };

            }
            catch (APIException)
            {
                return new Result<ArtistaResponse>()
                {
                    Message = "No se encontro el artista",
                    Success = false
                };
            }
            catch (Exception)
            {
                return new Result<ArtistaResponse>()
                {
                    Message = "Hubo un error, intenta denuevo mas tarde",
                    Success = false
                };
            }

        }
        public async Task<Result<MetaMusic.Data.Request.AlbumRequest>> GetAlbum(string albumId)
        {
            try
            {
                var config = SpotifyClientConfig.CreateDefault();

                var request = new ClientCredentialsRequest("be3b1a5218c449a79a3a21ddfe850a5f", "7cf1d67e3efd4690a1bd43a18903f7dc");
                var response = await new OAuthClient(config).RequestToken(request);

                var spotify = new SpotifyClient(config.WithToken(response.AccessToken));

                var album = await spotify.Albums.Get(albumId);
                MetaMusic.Data.Request.AlbumRequest albumARetornar = new MetaMusic.Data.Request.AlbumRequest();
                List<string> artistsIds = new List<string>();



                if (album is null)
                    return new Result<MetaMusic.Data.Request.AlbumRequest>()
                    {
                        Message = "No se encontro el album",
                        Success = false
                    };

                if (album.AlbumType != "album" )
                    return new Result<MetaMusic.Data.Request.AlbumRequest>()
                    {
                        Message = "No se encontro el album",
                        Success = false
                    };

                var existe = await dbContext.Albumes.FirstOrDefaultAsync(a => a.IdSpotify ==  album.Id && a.Publicado == true);
                if(existe is not null)
                    return new Result<MetaMusic.Data.Request.AlbumRequest>()
                    {
                        Message = "El album que quieres registrar ya existe",
                        Success = false
                    };


                if (album.Artists.Count() >= 1)
                {
                   
                    foreach (var artist in album.Artists)
                    {

                        var ar = await dbContext.Artistas.FirstOrDefaultAsync(a => a.SpotifyId == artist.Id);

                        if (ar is null)
                            return new Result<MetaMusic.Data.Request.AlbumRequest>()
                            { Message = $"""Uno de los artistas que estan en este album no se encuentran registrados. El id del artista es "{artist.Id}" y el nombre es "{artist.Name}"."""};

                        albumARetornar.Artistas.Add(new Album_Artista() { Artista = ar});

                        

                    }


                }

                
               albumARetornar.Fecha_Publicacion = album.ReleaseDate ?? "Indefinida";

                
              albumARetornar.Portada = album.Images[0].Url ?? "";

                if (album.Id == null)
                        return new Result<MetaMusic.Data.Request.AlbumRequest>()
                        { Message = $"No pudimos recuperar informacion importante del album. Este album no puede ser creado" };

                albumARetornar.IdSpotify = album.Id;

               
               albumARetornar.Nombre = album.Name ?? "Indefinido";

                if (album.Tracks != null && album.Tracks.Items is not null)
                    if (album.Tracks.Items.Count() >= 1)
                    {
                        foreach (var track in album.Tracks.Items)
                        {
                            albumARetornar.Tracks.Add(new Track() { Titulo = track.Name, Album = new Album(){Portada = album.Images[0].Url, } });
                        }
                    }

                return new Result<MetaMusic.Data.Request.AlbumRequest>()
                {
                    Data = albumARetornar,
                    Message = "Album Encontrado",
                    Success = true
                };
            }
            catch (APIException)
            {
                return new Result<MetaMusic.Data.Request.AlbumRequest>()
                {
                    Message = "No se encontro el artista o ID incorrecto",
                    Success = false
                };
            }
            catch (Exception)
            {
                return new Result<MetaMusic.Data.Request.AlbumRequest>()
                {
                    Message = "Hubo un error, intenta denuevo mas tarde",
                    Success = false
                };
            }
        }

        public async Task<Result<MetaMusic.Data.Request.AlbumRequest>> GetSingle(string albumId)
        {
            try
            {
                var config = SpotifyClientConfig.CreateDefault();

                var request = new ClientCredentialsRequest("be3b1a5218c449a79a3a21ddfe850a5f", "7cf1d67e3efd4690a1bd43a18903f7dc");
                var response = await new OAuthClient(config).RequestToken(request);

                var spotify = new SpotifyClient(config.WithToken(response.AccessToken));

                var album = await spotify.Albums.Get(albumId);
                MetaMusic.Data.Request.AlbumRequest albumARetornar = new MetaMusic.Data.Request.AlbumRequest();
                List<string> artistsIds = new List<string>();

                

                if (album is null)
                    return new Result<MetaMusic.Data.Request.AlbumRequest>()
                    {
                        Message = "No se encontro el album",
                        Success = false
                    };

                


                if(album.AlbumType != "single")
                    return new Result<MetaMusic.Data.Request.AlbumRequest>()
                    {
                        Message = "No se encontro el single",
                        Success = false
                    };

                var existe = await dbContext.Albumes.FirstOrDefaultAsync(a => a.IdSpotify == album.Id && a.Publicado == true);
                if (existe is not null)
                    return new Result<MetaMusic.Data.Request.AlbumRequest>()
                    {
                        Message = "El album que quieres registrar ya existe",
                        Success = false
                    };


                if (album.Artists.Count() >= 1)
                {

                    foreach (var artist in album.Artists)
                    {

                        var ar = await dbContext.Artistas.FirstOrDefaultAsync(a => a.SpotifyId == artist.Id);

                        if (ar is null)
                            return new Result<MetaMusic.Data.Request.AlbumRequest>()
                            { Message = $"""Uno de los artistas que estan en este album no se encuentran registrados. El id del artista es "{artist.Id}" y el nombre es "{artist.Name}".""" };

                        albumARetornar.Artistas.Add(new Album_Artista() { Artista = ar });



                    }


                }


                albumARetornar.Fecha_Publicacion = album.ReleaseDate ?? "Indefinida";


                albumARetornar.Portada = album.Images[0].Url ?? "";

                if (album.Id == null)
                    return new Result<MetaMusic.Data.Request.AlbumRequest>()
                    { Message = $"No pudimos recuperar informacion importante del album. Este album no puede ser creado" };

                albumARetornar.IdSpotify = album.Id;


                albumARetornar.Nombre = album.Name ?? "Indefinido";

               

                return new Result<MetaMusic.Data.Request.AlbumRequest>()
                {
                    Data = albumARetornar,
                    Message = "Album Encontrado",
                    Success = true
                };
            }
            catch (APIException)
            {
                return new Result<MetaMusic.Data.Request.AlbumRequest>()
                {
                    Message = "No se encontro el artista o ID incorrecto",
                    Success = false
                };
            }
            catch (Exception)
            {
                return new Result<MetaMusic.Data.Request.AlbumRequest>()
                {
                    Message = "Hubo un error, intenta denuevo mas tarde",
                    Success = false
                };
            }
        }


    }
}
