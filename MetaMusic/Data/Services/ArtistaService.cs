using MetaMusic.Data.Context;
using MetaMusic.Data.Entities;
using MetaMusic.Data.OtherEntities;
using MetaMusic.Data.Request;
using MetaMusic.Data.Responses;
using Microsoft.EntityFrameworkCore;

namespace MetaMusic.Data.Services
{
    public class ArtistaService : IArtistaService
    {
        private readonly IMetaMusicDbContext dbContext;
        private readonly IGoogleAuthService googleAuthService;

        public ArtistaService(IGoogleAuthService googleAuthService, IMetaMusicDbContext dbContext)
        {
            this.googleAuthService = googleAuthService;
            this.dbContext =
                dbContext;
        }

        public async Task<Result<ArtistaResponse>> CrearArtista(ArtistaRequest request)
        {
            try
            {
                if (request is null)
                    return new Result<ArtistaResponse>()
                    {
                        Message = "Bad Request",
                        Success = false
                    };

                List<Genero_Artista> generos = new List<Genero_Artista>();

                generos = request.GenerosMusicales.ToList();


                request.GenerosMusicales.Clear();
                var newArtist = Artista.Crear(request);
                await dbContext.Artistas.AddAsync(newArtist);
                await dbContext.SaveChangesAsync();

                var artist = await dbContext.Artistas.FirstOrDefaultAsync(a => a.Id == newArtist.Id);

                if (generos.Count() >= 0)
                {
                    foreach (var genero in generos)
                    {
                        var genre = await dbContext.Generos.FirstOrDefaultAsync(g => g.Id == genero.Id);

                        if (genre is not null)
                            dbContext.Genero_Artistas.Add(new Genero_Artista() { Artista = artist, Genero = genre });

                    };

                    await dbContext.SaveChangesAsync();
                }

                return new Result<ArtistaResponse>()
                {
                    Data = newArtist.ToResponse(),
                    Message = "Creacion Exitosa",
                    Success = true
                };

            }
            catch (Exception e)
            {
                return new Result<ArtistaResponse>()
                {

                    Message = e.InnerException?.Message ?? e.Message,
                    Success = false
                };
            }
        }
    }
}
