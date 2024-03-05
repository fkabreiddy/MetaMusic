using MetaMusic.Data.Context;
using MetaMusic.Data.Entities;
using MetaMusic.Data.OtherEntities;
using MetaMusic.Data.Request;
using MetaMusic.Data.Responses;
using MetaMusic.Pages.MainComponents;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using SpotifyAPI.Web;

namespace MetaMusic.Data.Services
{
    public class PeticionService : IPeticionService
    {
        private readonly IMetaMusicDbContext dbContext;
        private readonly IGoogleAuthService googleAuth;


        public PeticionService(IMetaMusicDbContext dbContext, IGoogleAuthService googleAuth)
        {
            this.dbContext = dbContext;
            this.googleAuth = googleAuth;

        }

        public async Task<Result<bool>> CrearArtista(string spotifyId)
        {
            try
            {

                var usuarioactual = await googleAuth.GetCurrentUser();

                if (usuarioactual.Data is null)
                    return new()
                    {
                        Message = "No estas logeado",
                        Success = false


                    };



                var usuario = await dbContext.Usuarios.FirstOrDefaultAsync(u => u.Id == usuarioactual.Data.Id);

                if (usuario is null)
                    return new Result<bool>()
                    {

                        Success = false,
                        Message = "Error creando tu peticion"
                    };
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

                var _peticionExistente = await dbContext.Peticiones.Include(p => p.Usuario).FirstOrDefaultAsync(p => p.ArtistaSpotifyId == spotifyId && p.Usuario.Id == usuario.Id);

                if (_peticionExistente is not null)
                {

                    
                    
                    return new Result<bool>() { Message = "Ya has pedido este artista", Success = false };

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
                peticion.Usuario = usuario;

                await dbContext.Peticiones.AddAsync(Peticion.Crear(peticion));

                await dbContext.SaveChangesAsync();

                return new() { Message = "Peticion Creada", Success = true };

            }
            catch (APIException)
            {
                return new() { Message = "No se encontro el artista", Success = false };

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
                var usuarioactual = await googleAuth.GetCurrentUser();

                if (usuarioactual.Data is null)
                    return new()
                    {
                        Message = "No estas logeado",
                        Success = false


                    };



                var usuario = await dbContext.Usuarios.FirstOrDefaultAsync(u => u.Id == usuarioactual.Data.Id);

                if (usuario is null)
                    return new Result<bool>()
                    {

                        Success = false,
                        Message = "Error creando tu peticion"
                    };
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

                var _peticionExistente = await dbContext.Peticiones.FirstOrDefaultAsync(p => p.AlbumSpotifyId == spotifyId && p.Usuario.Id == usuario.Id);

                if (_peticionExistente is not null)
                {
                   

                    return new Result<bool>() { Message = "Ya has pedido este album", Success = false };

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

                peticion.Usuario = usuario;
                peticion.AlbumSpotifyId = spotifyId;

                await dbContext.Peticiones.AddAsync(Peticion.Crear(peticion));

                await dbContext.SaveChangesAsync();

                return new() { Message = "Peticion Creada", Success = true };

            }
            catch(APIException )
            {
                return new() { Message = "No se encontro el album", Success = false };

            }
            catch (Exception ex)
            {
                return new() { Message = ex.InnerException?.Message ?? ex.Message, Success = false };

            }
        }

        
        public async Task<Result<List<PeticionResponse>>> ConsultarTodo()
        {
           try {
                var peticionesADevolver = new List<PeticionResponse>();

                // Agrupar por ArtistaSpotifyId
                var artistas = await dbContext.Peticiones
                    .Include(p => p.Usuario).Where(a => a.ArtistaSpotifyId != null)
                    
                    .ToListAsync();

                foreach (var artista in artistas)
                {
                    var peticion = peticionesADevolver.FirstOrDefault(p => p.ArtistaSpotifyId == artista.ArtistaSpotifyId);

                    if(peticion is null)
                    {
                        peticionesADevolver.Add(artista.ToResponse());
                    }
                    else
                    {
                        peticion.Acumulaciones += 1;
                    }
                   
                }

                // Agrupar por AlbumSpotifyId
                var albumes = await dbContext.Peticiones
                    .Include(p => p.Usuario).Where(a => a.AlbumSpotifyId != null)
                   
                    .ToListAsync();

                foreach (var album in albumes)
                {
                    var peticion = peticionesADevolver.FirstOrDefault(a => a.AlbumSpotifyId == album.AlbumSpotifyId);

                    if(peticion is null)
                    {
                        peticionesADevolver.Add(album.ToResponse());
                    }
                    else
                    {
                        peticion.Acumulaciones += 1;
                    }
                }

                return new Result<List<PeticionResponse>>()
                {
                    Message = "Éxito",
                    Data = peticionesADevolver,
                    Success = true
                };
            }
            catch (Exception ex)
           {
                return new Result<List<PeticionResponse>>()
                {
                    Message = ex.InnerException?.Message ?? ex.Message,
                    Success = false
                };
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
