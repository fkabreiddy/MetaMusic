using MetaMusic.Data.OtherEntities;
using MetaMusic.Data.Request;
using MetaMusic.Data.Responses;

namespace MetaMusic.Data.Services
{
    public interface IGeneroService
    {
        Task<Result<bool>> Eliminar(GeneroRequest request);
        Task<Result<List<GeneroResponse>>> GetGeneros();
        Task<Result<GeneroResponse>> GetOne(GeneroRequest request);
    }
}