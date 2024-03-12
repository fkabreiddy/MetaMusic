using MetaMusic.Data.Context;
using MetaMusic.Data.OtherEntities;
using MetaMusic.Data.Request;
using MetaMusic.Data.Entities;
using MetaMusic.Data.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;


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

        [Authorize(Roles = "Staff")]
        public async Task<Result<GeneroResponse>> Crear(GeneroRequest request)
        {
            try
            {
                var genero = await dbContext.Generos.FirstOrDefaultAsync(g => g.Nombre.ToLower() == request.Nombre.ToLower()) ;

                if (genero is not null)
                    return new()
                    {
                        Message = "No Ese genero ya existe",
                        Success = false
                    };

                var newgenero = Genero.Crear(request);

                await dbContext.Generos.AddAsync(newgenero);

                await dbContext.SaveChangesAsync();


                var generonuevo = await dbContext.Generos.Include(g => g.Artistas).FirstOrDefaultAsync(g => g.Id == newgenero.Id);

                if (generonuevo is null)
                    return new() { Success = false, Message = "No se pudo crear el genero" };

                return new ()
                {
                    Data = generonuevo.ToResponse() ,
                    Message = "Exito",
                    Success = true
                };

            }
            catch (Exception ex)
            {
                return new ()
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

        public async Task<Result<bool>> Eliminar(int Id)
        {
            try
            {
                var genero = await dbContext.Generos.FirstOrDefaultAsync(g => g.Id == Id);

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
    }
}
