using MetaMusic.Data.Context;
using MetaMusic.Data.Entities;
using MetaMusic.Data.OtherEntities;
using MetaMusic.Data.Request;
using MetaMusic.Pages.MainComponents;
using Microsoft.EntityFrameworkCore;
using SpotifyAPI.Web;

namespace MetaMusic.Data.Services
{
    public class PeticionService : IPeticionService
    {
        private readonly IMetaMusicDbContext dbContext;



        public PeticionService(IMetaMusicDbContext dbContext)
        {
            this.dbContext = dbContext;

        }

        public async Task<Result<bool>> CrearArtista(string spotifyId)
        {
            try
            {
                var config = SpotifyClientConfig.CreateDefault();

                var request = new ClientCredentialsRequest("be3b1a5218c449a79a3a21ddfe850a5f", "7cf1d67e3efd4690a1bd43a18903f7dc");
                var response = await new OAuthClient(config).RequestToken(request);

                var spotify = new SpotifyClient(config.WithToken(response.AccessToken));

                var artist = await spotify.Artists.Get(spotifyId);

                if (artist is null)
                    return new()
                    {
                        Message = "El artista no pudo ser encontrado",
                        Success = false
                    };

                var _peticionExistente = await dbContext.Peticiones.FirstOrDefaultAsync(p => p.ArtistaSpotifyId == spotifyId);

                if (_peticionExistente is not null)
                {
                    _peticionExistente.Aumentar();
                    await dbContext.SaveChangesAsync();

                    return new Result<bool>() { Message = "Peticion creada", Success = true };

                }



                PeticionRequest peticion = new PeticionRequest();
                if (artist.Images[0] is not null)
                {
                    peticion.ArtistaFoto = artist.Images[0].Url;
                }

                if (artist.Name is not null)
                {
                    peticion.ArtistaNombre = artist.Name;
                }

                peticion.ArtistaSpotifyId = spotifyId;

                await dbContext.Peticiones.AddAsync(Peticion.Crear(peticion));

                await dbContext.SaveChangesAsync();

                return new() { Message = "Peticion Creada", Success = true };

            }
            catch (Exception ex)
            {
                return new() { Message = ex.InnerException?.Message ?? ex.Message, Success = false };

            }
        }

        public async Task<Result<bool>> CrearAlbum(string spotifyId)
        {
            try
            {
                var config = SpotifyClientConfig.CreateDefault();

                var request = new ClientCredentialsRequest("be3b1a5218c449a79a3a21ddfe850a5f", "7cf1d67e3efd4690a1bd43a18903f7dc");
                var response = await new OAuthClient(config).RequestToken(request);

                var spotify = new SpotifyClient(config.WithToken(response.AccessToken));

                var album = await spotify.Albums.Get(spotifyId);

                if (album is null)
                    return new()
                    {
                        Message = "El album no pudo ser encontrado",
                        Success = false
                    };

                var _peticionExistente = await dbContext.Peticiones.FirstOrDefaultAsync(p => p.AlbumSpotifyId == spotifyId);

                if (_peticionExistente is not null)
                {
                    _peticionExistente.Aumentar();
                    await dbContext.SaveChangesAsync();

                    return new Result<bool>() { Message = "Peticion creada", Success = true };

                }



                PeticionRequest peticion = new PeticionRequest();
                if (album.Images[0] is not null)
                {
                    peticion.AlbumPortada = album.Images[0].Url;
                }

                if (album.Name is not null)
                {
                    peticion.AlbumNombre = album.Name;
                }

                peticion.AlbumSpotifyId = spotifyId;

                await dbContext.Peticiones.AddAsync(Peticion.Crear(peticion));

                await dbContext.SaveChangesAsync();

                return new() { Message = "Peticion Creada", Success = true };

            }
            catch (Exception ex)
            {
                return new() { Message = ex.InnerException?.Message ?? ex.Message, Success = false };

            }
        }


        public async Task<Result<bool>> Eliminar(int id)
        {
            try
            {

                var _peticionExistente = await dbContext.Peticiones.FirstOrDefaultAsync(p => p.Id == id);


                if (_peticionExistente is null)
                    return new() { Message = "Peticion no entontrada", Success = false };


                dbContext.Peticiones.Remove(_peticionExistente);


                await dbContext.SaveChangesAsync();

                return new() { Message = "Peticion Eliminada", Success = true };

            }
            catch (Exception ex)
            {
                return new() { Message = ex.InnerException?.Message ?? ex.Message, Success = false };

            }
        }
    }
}
