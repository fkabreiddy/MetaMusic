using MetaMusic.Data.OtherEntities;
using MetaMusic.Data.Request;
using MetaMusic.Data.Responses;
using Microsoft.AspNetCore.Authorization;

namespace MetaMusic.Data.Services
{
    public interface IGeneroService
    {
        Task<Result<bool>> Eliminar(int Id);
        Task<Result<List<GeneroResponse>>> GetGeneros();
        Task<Result<GeneroResponse>> GetOne(GeneroRequest request);

        [Authorize(Roles = "Staff")]
        Task<Result<GeneroResponse>> Crear(GeneroRequest request);
    }
}