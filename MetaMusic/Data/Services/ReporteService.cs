using MetaMusic.Data.Context;
using MetaMusic.Data.Entities;
using MetaMusic.Data.OtherEntities;
using MetaMusic.Data.Request;
using MetaMusic.Data.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;

namespace MetaMusic.Data.Services
{
    public class ReporteService : IReporteService
    {
        private readonly IMetaMusicDbContext dbContext;
        private readonly IGoogleAuthService googleAuthService;

        public ReporteService(IMetaMusicDbContext dbContext, IGoogleAuthService googleAuthService)
        {
            this.dbContext = dbContext;
            this.googleAuthService = googleAuthService;
        }

     
        public async Task<Result<bool>> ReportarReview(int review, string motivo, int severidad)
        {
            try
            {

                var currentuser = await googleAuthService.GetCurrentUser();

                if (currentuser.Data == null)
                {

                    return new Result<bool>()
                    {
                        Success = false,
                        Message = "No estas logeado"
                    };

                }

                var usuarioResponse = await dbContext.Usuarios.FirstOrDefaultAsync(u => u.Id == currentuser.Data.Id);

                if (usuarioResponse == null)
                {

                    return new Result<bool>()
                    {
                        Success = false,
                        Message = "No estas registrado"
                    };

                }

                var existe = await dbContext.Reportes.FirstOrDefaultAsync(r => r.Review!.Id == review && r.Usuario!.Id == usuarioResponse.Id);

                if (existe is not null)
                {
                    return new Result<bool>()
                    {
                        Success = false,
                        Message = "Ya has hecho un reporte de esta review el cual está en proceso."
                    };
                }
                var reviewResponse = await dbContext.Reviews.Include(r => r.Album).FirstOrDefaultAsync(r => r.Id == review);

                if (reviewResponse is null)
                {
                    return new Result<bool>()
                    {
                        Success = false,
                        Message = "Review no encontrada"
                    };
                }
                var nuevoReporte = new ReporteRequest() { Contenido = $"{motivo}", Titulo = "Reporte a una review tuya.", Review = reviewResponse, Usuario = usuarioResponse, Severidad = severidad, Nota = null };
                await dbContext.Reportes.AddAsync(Reporte.Crear(nuevoReporte));

                await dbContext.SaveChangesAsync();



                return new Result<bool>()
                {
                    Success = true,
                    Message = "se envio tu reporte correctamente",
                    Data = true
                };


            }
            catch (Exception ex)
            {


                return new Result<bool>()
                {
                    Success = false,
                    Message = ex.InnerException?.Message ?? ex.Message,

                };

            }
        }
        public async Task<Result<bool>> ReportarNota(int nota, string motivo, int severidad)
        {
            try
            {
                var currentuser = await googleAuthService.GetCurrentUser();

                if (currentuser.Data == null)
                {

                    return new Result<bool>()
                    {
                        Success = false,
                        Message = "No estas logeado"
                    };

                }

                var usuarioResponse = await dbContext.Usuarios.FirstOrDefaultAsync(u => u.Id == currentuser.Data.Id);

                if (usuarioResponse == null)
                {

                    return new Result<bool>()
                    {
                        Success = false,
                        Message = "No estas registrado"
                    };

                }
                var notaResponse = await dbContext.Notas.Include(r => r.Album).FirstOrDefaultAsync(r => r.Id == nota);

                if (notaResponse is null)
                {
                    return new Result<bool>()
                    {
                        Success = false,
                        Message = "Nota no encontrada"
                    };
                }

                await dbContext.Reportes.AddAsync(Reporte.Crear(new ReporteRequest() { Contenido = $"{motivo}", Titulo = "Reporte a una nota.", Nota = notaResponse, Usuario = usuarioResponse, Severidad = severidad, Review=null }));

                await dbContext.SaveChangesAsync();



                return new Result<bool>()
                {
                    Success = true,
                    Message = "se envio tu reporte correctamente",
                    Data = true
                };


            }
            catch (Exception ex)
            {


                return new Result<bool>()
                {
                    Success = false,
                    Message = ex.InnerException?.Message ?? ex.Message,

                };

            }
        }

        public async Task<Result<List<ReporteResponse>>> Consultar(int currentuserId)
        {
            try
            {


                var reportes = await dbContext.Reportes.Include(r => r.Usuario).Include(r => r.Review).ThenInclude(re => re!.Album).Include(r => r.Review).ThenInclude(re => re!.Creador).Include(r => r.Nota).ThenInclude(n => n!.Creador).Where(r => r.Review!.Album!.Creador!.Id  == currentuserId || r.Nota!.Id != 0 ).ToListAsync();

                if (reportes is null)
                {
                    return new Result<List<ReporteResponse>>()
                    {
                        Success = false,
                        Message = "No hay reportes"
                    };
                }



                return new Result<List<ReporteResponse>>()
                {
                    Success = true,

                    Data = reportes.Select(r => r.ToResponse()).ToList()
                };


            }
            catch (Exception ex)
            {


                return new Result<List<ReporteResponse>>()
                {
                    Success = false,
                    Message = ex.InnerException?.Message ?? ex.Message,

                };

            }
        }
        public async Task<Result<bool>> Eliminar(int reporte)
        {
            try
            {


                var reporteResponse = await dbContext.Reportes.FirstOrDefaultAsync(r => r.Id == reporte);

                if (reporteResponse is null)
                {
                    return new Result<bool>()
                    {
                        Success = false,
                        Message = "Reporte no encontrada"
                    };
                }

                dbContext.Reportes.Remove(reporteResponse);

                await dbContext.SaveChangesAsync();



                return new Result<bool>()
                {
                    Success = true,
                    Message = "se elimino el reporte.",
                    Data = true
                };


            }
            catch (Exception ex)
            {


                return new Result<bool>()
                {
                    Success = false,
                    Message = ex.InnerException?.Message ?? ex.Message,

                };

            }
        }
    }
}
