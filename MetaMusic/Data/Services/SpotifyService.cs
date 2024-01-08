using MetaMusic.Data.Context;
using MetaMusic.Data.OtherEntities;
using MetaMusic.Data.Entities;
using MetaMusic.Data.Responses;
using SpotifyAPI.Web;

namespace MetaMusic.Data.Services
{
    public class SpotifyService : ISpotifyService
    {



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
            catch (APIException e)
            {
                return new Result<ArtistaResponse>()
                {
                    Message = "No se encontro el artista",
                    Success = false
                };
            }
            {

            }
        }
        public async Task<Result<AlbumResponse>> GetAlbum(string albumId)
        {
            try
            {
                var config = SpotifyClientConfig.CreateDefault();

                var request = new ClientCredentialsRequest("be3b1a5218c449a79a3a21ddfe850a5f", "7cf1d67e3efd4690a1bd43a18903f7dc");
                var response = await new OAuthClient(config).RequestToken(request);

                var spotify = new SpotifyClient(config.WithToken(response.AccessToken));

                var album = await spotify.Albums.Get(albumId);
                AlbumResponse albumARetornar = new AlbumResponse();
                List<string> artistsIds = new List<string>();



                if (album is null)
                    return new Result<AlbumResponse>()
                    {
                        Message = "No se encontro el artista",
                        Success = false
                    };

                if (album.Artists.Count() >= 1)
                {
                    foreach (var artist in album.Artists)
                    {
                        Album_Artista relacion = new Album_Artista();
                        relacion.Artista = new Artista();
                        relacion.Artista.SpotifyId = artist.Id;
                        albumARetornar.Artistas.Add(relacion);
                    }
                }

                if (album.ReleaseDate != null)
                    albumARetornar.Fecha_Publicacion = album.ReleaseDate;

                if (album.Images.Count() >= 1)
                    albumARetornar.Portada = album.Images[0].Url;

                if (album.Id != null)
                    albumARetornar.IdSpotify = album.Id;

                if (album.Name != null)
                    albumARetornar.Nombre = album.Name;

                if (album.Tracks != null)
                    if (album.Tracks.Items.Count() >= 1)
                    {
                        foreach (var track in album.Tracks.Items)
                        {
                            albumARetornar.Tracks.Add(new Track() { Titulo = track.Name });
                        }
                    }

                return new Result<AlbumResponse>()
                {
                    Data = albumARetornar,
                    Message = "Album Encontrado",
                    Success = false
                };
            }
            catch (APIException e)
            {
                return new Result<AlbumResponse>()
                {
                    Message = "No se encontro el artista",
                    Success = false
                };
            }
            {

            }
        }


    }
}
