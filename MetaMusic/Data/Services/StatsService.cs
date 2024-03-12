using MetaMusic.Data.Context;
using MetaMusic.Data.Entities;
using MetaMusic.Data.OtherEntities;
using MetaMusic.Data.Responses;
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

                Dictionary<Genero, int> resultado = new Dictionary<Genero, int>();
                var generos = await dbContext.Generos.Include(g => g.Artistas).ToListAsync();

                if (generos is null)
                    return new() { Success = false, Message = "No hay generos" };

               
                foreach(var genero in generos )
                {
                    resultado.Add(genero, genero.Artistas.Count());
                }


                return new()
                {
                    Success = true,
                    Message = "Exito",
                    Data = resultado.OrderByDescending(g => g.Value).ToDictionary()
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
        public async Task<Result<List<AlbumResponse>>> AlbumesPerYear(int year)
        {
            try
            {
                var userContext = await googleAuth.GetCurrentUser();


                if (userContext is null || userContext.Data is null || userContext.Data.Rol.Tipo != "Staff")
                {
                    return new() { Message = "No estas autorizado o no estas logeado para ver esto", Success = false };


                }

                var albumes = await dbContext.Albumes.Where(a => a.Fecha_Agregado.Year == year).ToListAsync();

                return new()
                {
                    Success = true,
                    Message = "Exito",
                    Data = albumes.Select(a => a.ToResponse()).ToList()



                };

            }
            catch (Exception e)
            {
                return new Result<List<AlbumResponse>>()
                {
                    Message = e.InnerException?.Message ?? e.Message,
                    Success = false
                };
            }
        }

        public async Task<Result<List<AlbumResponse>>> MyAlbums(int year)
        {
            try
            {
                var userContext = await googleAuth.GetCurrentUser();


                if (userContext is null || userContext.Data is null || userContext.Data.Rol.Tipo != "Staff")
                {
                    return new() { Message = "No estas autorizado o no estas logeado para ver esto", Success = false };


                }

                var albumes = await dbContext.Albumes.Where(a => a.Creador!.Id == userContext.Data.Id).ToListAsync();

                return new()
                {
                    Success = true,
                    Message = "Exito",
                    Data = albumes.Select(a => a.ToResponse()).ToList()



                };

            }
            catch (Exception e)
            {
                return new Result<List<AlbumResponse>>()
                {
                    Message = e.InnerException?.Message ?? e.Message,
                    Success = false
                };
            }
        }

        public async Task<Result<Dictionary<UsuarioResponse, int>>> TopMostActiveUsers(int year)
        {
            try
            {
                var userContext = await googleAuth.GetCurrentUser();

                



                if (userContext is null || userContext.Data is null || userContext.Data.Rol.Tipo != "Staff")
                {
                    return new() { Message = "No estas autorizado o no estas logeado para ver esto", Success = false };


                }

                Dictionary<UsuarioResponse, int> resultado = new Dictionary<UsuarioResponse, int>();

                var usuarios = await dbContext.Usuarios.Include(u => u.Calificaciones).OrderBy(u => u.Calificaciones.Count).Take(10).ToListAsync();


                foreach(var usuario in usuarios)
                {
                    resultado.Add(usuario.ToResponse(), usuario.Calificaciones.Count());
                }
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
