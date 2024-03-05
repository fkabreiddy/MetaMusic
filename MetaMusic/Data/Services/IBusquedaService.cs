using MetaMusic.Data.OtherEntities;
using MetaMusic.Data.Responses;

namespace MetaMusic.Data.Services
{
    public interface IBusquedaService
    {
        Task<Result<List<BusquedaResponse>>> ConsultarMios();
        Task<Result<List<BusquedaResponse>>> Consultar(string tema);
        Task<Result<bool>> Crear(string tema);
        Task<Result<bool>> Eliminar(int id);
    }
}