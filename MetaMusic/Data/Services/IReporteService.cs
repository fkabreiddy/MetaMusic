using MetaMusic.Data.OtherEntities;
using MetaMusic.Data.Responses;

namespace MetaMusic.Data.Services
{
    public interface IReporteService
    {
        Task<Result<List<ReporteResponse>>> Consultar(int currentuserId);
        Task<Result<bool>> Eliminar(int reporte);
        Task<Result<bool>> ReportarNota(int nota, string motivo, int severidad);
        Task<Result<bool>> ReportarReview(int review, string motivo, int severidad);
    }
}