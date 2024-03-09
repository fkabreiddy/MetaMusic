using MetaMusic.Data.Context;
using MetaMusic.Data.Entities;
using MetaMusic.Data.OtherEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace MetaMusic.Data.Services
{
    public class StatsService : IStatsService
    {
        private readonly IMetaMusicDbContext dbContext;
        private readonly IGoogleAuthService googleAuth;

        public StatsService(IMetaMusicDbContext dbContext, IGoogleAuthService googleAuth)
        {
            this.dbContext = dbContext;
            this.googleAuth = googleAuth;
        }


        public async Task<Result<Dictionary<string, int>>> SinglesAndAlbums()
        {
            try
            {
                var userContext = await googleAuth.GetCurrentUser();


                if (userContext is null || userContext.Data is null || userContext.Data.Rol.Tipo != "Staff")
                {
                    return new() { Message = "No estas autorizado o no estas logeado para ver esto", Success = false };


                }
                var resultado = new Dictionary<string, int>();

                var albumes = await dbContext.Albumes.Where(a => a.IsSingle == false).ToListAsync();

                if (albumes is null)
                {
                    resultado.Add("albumes", 0);

                }
                else
                {
                    resultado.Add("albumes", albumes.Count);

                }


                var singles = await dbContext.Albumes.Where(a => a.IsSingle == true).ToListAsync();


                if (singles is null)
                {
                    resultado.Add("singles", 0);

                }
                else
                {
                    resultado.Add("singles", singles.Count);

                }


                return new() { Data = resultado, Message = "Exito", Success = true };

            }
            catch (Exception e)
            {
                return new()
                {
                    Message = e.InnerException?.Message ?? e.Message,
                    Success = false
                };
            }
        }

        public async Task<Result<Dictionary<Genero, int>>> RankGenerosArtistas()
        {
            try
            {
                var userContext = await googleAuth.GetCurrentUser();


                if (userContext is null || userContext.Data is null || userContext.Data.Rol.Tipo != "Staff")
                {
                    return new() { Message = "No estas autorizado o no estas logeado para ver esto", Success = false };


                }


                var generos = await dbContext.Genero_Artistas.ToListAsync();

                if (generos is null)
                    return new() { Success = false, Message = "No hay generos" };

                var resultado = generos
                        .GroupBy(ga => ga.Genero ?? new Genero())
                        .ToDictionary(
                            group => group.Key,
                            group => group.Count()
                        );



                return new()
                {
                    Success = true,
                    Message = "Exito",
                    Data = resultado
                };

            }
            catch (Exception e)
            {
                return new()
                {
                    Message = e.InnerException?.Message ?? e.Message,
                    Success = false
                };
            }
        }
        public async Task<Result<Dictionary<Genero, int>>> RankGenerosAlbumes()
        {
            try
            {
                var userContext = await googleAuth.GetCurrentUser();


                if (userContext is null || userContext.Data is null || userContext.Data.Rol.Tipo != "Staff")
                {
                    return new() { Message = "No estas autorizado o no estas logeado para ver esto", Success = false };


                }


                var generos = await dbContext.Genero_Artistas.ToListAsync();
                var albumes = await dbContext.Albumes.ToListAsync();

                if (generos is null || albumes is null)
                    return new() { Success = false, Message = "No hay datos" };

                var resultado = albumes
     .Join(
         generos,
         album => album.Artistas[0].Id,  // Clave de unión con ArtistaId de Album
         generoArtista => generoArtista.Artista.Id,
         (album, generoArtista) => new { Genero = generoArtista.Genero, Album = album }
     )
     .GroupBy(ag => ag.Genero)
     .ToDictionary(
         group => group.Key,
         group => group.Count()
     );



                return new()
                {
                    Success = true,
                    Message = "Exito",
                    Data = resultado
                };

            }
            catch (Exception e)
            {
                return new()
                {
                    Message = e.InnerException?.Message ?? e.Message,
                    Success = false
                };
            }
        }
    }
}
