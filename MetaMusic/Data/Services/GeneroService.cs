using MetaMusic.Data.Context;
using MetaMusic.Data.OtherEntities;
using MetaMusic.Data.Request;
using MetaMusic.Data.Responses;
using Microsoft.EntityFrameworkCore;
using System.Linq.Dynamic.Core;

namespace MetaMusic.Data.Services
{
    public class GeneroService : IGeneroService
    {
        private readonly IMetaMusicDbContext dbContext;

        public GeneroService(IMetaMusicDbContext dbContext)
        {
            this.dbContext = dbContext;
        }


        public async Task<Result<List<GeneroResponse>>> GetGeneros()
        {
            try
            {
                var generos = await dbContext.Generos.ToListAsync();

                if (generos is null)
                    return new Result<List<GeneroResponse>>()
                    {
                        Message = "No hay generos en la base de datos",
                        Success = false
                    };

                return new Result<List<GeneroResponse>>()
                {
                    Data = generos.Select(G => G.ToResponse()).ToList(),
                    Message = "Exito",
                    Success = true
                };

            }
            catch (Exception ex)
            {
                return new Result<List<GeneroResponse>>()
                {

                    Message = ex.InnerException?.Message ?? ex.Message,
                    Success = false
                };
            }
        }

        public async Task<Result<GeneroResponse>> GetOne(GeneroRequest request)
        {
            try
            {
                var genero = await dbContext.Generos.FirstOrDefaultAsync(g => g.Id == request.Id);

                if (genero is null)
                    return new Result<GeneroResponse>()
                    {
                        Message = "El genero no existe",
                        Success = false
                    };

                return new Result<GeneroResponse>()
                {
                    Data = genero.ToResponse(),
                    Message = "Exito",
                    Success = false
                };

            }
            catch (Exception ex)
            {
                return new Result<GeneroResponse>()
                {

                    Message = ex.InnerException?.Message ?? ex.Message,
                    Success = false
                };
            }
        }

        public async Task<Result<bool>> Eliminar(GeneroRequest request)
        {
            try
            {
                var genero = await dbContext.Generos.FirstOrDefaultAsync(g => g.Id == request.Id);

                if (genero is null)
                    return new Result<bool>()
                    {
                        Message = "El genero no existe",
                        Success = false
                    };

                dbContext.Generos.Remove(genero);

                await dbContext.SaveChangesAsync();
                return new Result<bool>()
                {
                    Data = true,
                    Message = "Exito",
                    Success = false
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
    }
}
