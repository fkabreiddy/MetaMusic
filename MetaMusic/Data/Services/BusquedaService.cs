using MetaMusic.Data.Context;
using MetaMusic.Data.Entities;
using MetaMusic.Data.OtherEntities;
using MetaMusic.Data.Request;
using MetaMusic.Data.Responses;
using Microsoft.EntityFrameworkCore;

namespace MetaMusic.Data.Services
{
    public class BusquedaService : IBusquedaService
    {

        private readonly IMetaMusicDbContext dbContext;
        private readonly IGoogleAuthService googleAuthService;

        public BusquedaService(IMetaMusicDbContext dbContext, IGoogleAuthService googleAuthService)
        {
            this.dbContext = dbContext;
            this.googleAuthService = googleAuthService;
        }


        public async Task<Result<bool>> Crear(string tema)
        {

            try
            {
                var UsuarioActual = await googleAuthService.GetCurrentUser();

                if (UsuarioActual.Data is null)
                    return new() { Message = "No estas logeado", Success = false };


                var usuario = await dbContext.Usuarios.FirstOrDefaultAsync(u => u.Id == UsuarioActual.Data.Id);



                if (usuario is null)
                    return new() { Success = false, Message = "No estas registrado en al app" };

                var existe = await dbContext.Busquedas.FirstOrDefaultAsync(b => b.Usuario!.Id == usuario.Id && b.Contenido == tema);

                if (existe is not null)
                    return new()
                    {
                        Success = false
                    };

                var newbusqueda = Busqueda.Crear(new BusquedaRequest() { Contenido = tema, Usuario = usuario });

                await dbContext.Busquedas.AddAsync(newbusqueda);

                await dbContext.SaveChangesAsync();

                return new() { Message = "Exito", Success = true };


            }
            catch (Exception e)
            {
                return new()
                {
                    Success = false,
                    Message = e.InnerException?.Message ?? e.Message
                };
            }
        }

        public async Task<Result<List<BusquedaResponse>>> ConsultarMios()
        {

            try
            {
                var UsuarioActual = await googleAuthService.GetCurrentUser();

                if (UsuarioActual.Data is null)
                    return new() { Message = "No estas logeado", Success = false };


                var usuario = await dbContext.Usuarios.FirstOrDefaultAsync(u => u.Id == UsuarioActual.Data.Id);


                if (usuario is null)
                    return new() { Success = false, Message = "No estas registrado en al app" };

                

                var busquedas = await dbContext.Busquedas.Where(b => b.Usuario!.Id == usuario.Id).ToListAsync();

                if (busquedas is null)
                    return new()
                    {
                        Message = "No hay busquedas tuyas",
                        Success = false
                    };

                return new() { Message = "Exito", Success = true, Data = busquedas.Select(b => b.ToResponse()).ToList() };


            }
            catch (Exception e)
            {
                return new()
                {
                    Success = false,
                    Message = e.InnerException?.Message ?? e.Message
                };
            }
        }

        public async Task<Result<List<BusquedaResponse>>> Consultar(string tema)
        {

            try
            {


                var busquedas = await dbContext.Busquedas.Where(b => b.Contenido.ToLower().Contains(tema.ToLower())).GroupBy(b => b.Contenido).Select(b => b.FirstOrDefault()).ToListAsync();

                if (busquedas is null)
                    return new()
                    {
                        Message = "No hay busquedas tuyas",
                        Success = false
                    };

                return new() { Message = "Exito", Success = true, Data = busquedas.Select(b => b!.ToResponse()).ToList() };


            }
            catch (Exception e)
            {
                return new()
                {
                    Success = false,
                    Message = e.InnerException?.Message ?? e.Message
                };
            }
        }

        public async Task<Result<bool>> Eliminar(int id)
        {

            try
            {
                var UsuarioActual = await googleAuthService.GetCurrentUser();

                if (UsuarioActual.Data is null)
                    return new() { Message = "No estas logeado", Success = false };


                var usuario = await dbContext.Usuarios.FirstOrDefaultAsync(u => u.Id == UsuarioActual.Data.Id);


                if (usuario is null)
                    return new() { Success = false, Message = "No estas registrado en al app" };


                var busqueda = await dbContext.Busquedas.FirstOrDefaultAsync(b => b.Id == id);

                if (busqueda is null)
                    return new()
                    {
                        Message = "No Existe esta busqueda tuya",
                        Success = false
                    };


                dbContext.Busquedas.Remove(busqueda);
                await dbContext.SaveChangesAsync();

                return new() { Message = "Exito", Success = true };


            }
            catch (Exception e)
            {
                return new()
                {
                    Success = false,
                    Message = e.InnerException?.Message ?? e.Message
                };
            }
        }


    }
}
