using MetaMusic.Data.Context;
using MetaMusic.Data.Entities;
using MetaMusic.Data.OtherEntities;
using MetaMusic.Data.Request;
using MetaMusic.Data.Responses;
using Microsoft.EntityFrameworkCore;
using System.Runtime.InteropServices;

namespace MetaMusic.Data.Services
{
    public class NotaService : INotaService
    {
        private readonly IMetaMusicDbContext dbContext;
        private readonly IGoogleAuthService googleAuth;

        public NotaService(IMetaMusicDbContext dbContext, IGoogleAuthService googleAuth)
        {
            this.dbContext = dbContext;
            this.googleAuth = googleAuth;
        }


        public async Task<Result<NotaResponse>> Crear(NotaRequest request, AlbumRequest albumrequest)
        {

            try
            {
                var currentUser = await googleAuth.GetCurrentUser();

                if (currentUser.Data is null)
                    return new Result<NotaResponse>() { Message = "No estas logeado", Success = false };

                var usuarioactual = await dbContext.Usuarios.FirstOrDefaultAsync(u => u.Id == currentUser.Data.Id);

                if (usuarioactual is null)
                    return new Result<NotaResponse>() { Message = "Primero crea una cuenta", Success = false };

                var album = await dbContext.Albumes.FirstOrDefaultAsync(a => a.Id == albumrequest.Id);

                if (album is null)
                    return new Result<NotaResponse>() { Message = "El album no existe", Success = false };

            
                request.Album = album;
                request.Creador = usuarioactual;

                var notaacrear = Nota.Crear(request);
                await dbContext.Notas.AddAsync(notaacrear);
               

                await dbContext.SaveChangesAsync();

               
                return new Result<NotaResponse>()
                {
                    Data = notaacrear.ToResponse(),
                    Message = "Nota publicada",
                    Success = true
                };





            }
            catch (Exception e)
            {
                return new Result<NotaResponse>()
                {

                    Message = e.InnerException?.Message ?? e.Message,
                    Success = false
                };
            }
        }
        public async Task<Result<bool>> Eliminar(int notaid)
        {

            try
            {



                var nota = await dbContext.Notas.FirstOrDefaultAsync(a => a.Id == notaid);

                if (nota is null)
                    return new Result<bool>() { Message = "La nota no existe", Success = false };



                dbContext.Notas.Remove(nota);


                await dbContext.SaveChangesAsync();




                return new Result<bool>()
                {

                    Message = "Nota publicada",
                    Success = true
                };





            }
            catch (Exception e)
            {
                return new Result<bool>()
                {

                    Message = e.InnerException?.Message ?? e.Message,
                    Success = false
                };
            }
        }
        public async Task<Result<List<NotaResponse>>> Consultar(int albumid)
        {

            try
            {


                var notas = await dbContext.Notas.Include(n => n.Creador).Include(n => n.Usuarios_Liked).ThenInclude(l => l.Usuario).Include(n => n.Album).Include(l => l.Usuarios_DisLiked).ThenInclude(r => r.Usuario).Where(a => a.Album!.Id == albumid).ToListAsync();

                if (notas is null)
                    return new Result<List<NotaResponse>>() { Message = "El album no tiene notas", Success = false };


                return new Result<List<NotaResponse>>()
                {
                    Data = notas.Select(n => n.ToResponse()).ToList(),
                    Message = "Exito",
                    Success = true
                };





            }
            catch (Exception e)
            {
                return new Result<List<NotaResponse>>()
                {

                    Message = e.InnerException?.Message ?? e.Message,
                    Success = false
                };
            }
        }

        public async Task<Result<List<NotaResponse>>> ConsulatNotasDelUsuario(int userid)
        {

            try
            {


                var notas = await dbContext.Notas.Include(n => n.Creador).Include(n => n.Usuarios_Liked).ThenInclude(l => l.Usuario).Include(l => l.Usuarios_DisLiked).ThenInclude(r => r.Usuario).Include(n => n.Album).Where(a => a.Creador!.Id == userid).ToListAsync();

                if (notas is null)
                    return new Result<List<NotaResponse>>() { Message = "El album no tiene notas", Success = false };


                return new Result<List<NotaResponse>>()
                {
                    Data = notas.Select(n => n.ToResponse()).ToList(),
                    Message = "Exito",
                    Success = true
                };





            }
            catch (Exception e)
            {
                return new Result<List<NotaResponse>>()
                {

                    Message = e.InnerException?.Message ?? e.Message,
                    Success = false
                };
            }
        }
        public async Task<Result<NotaResponse>> LikeNota(int notaid, int userid)
        {

            try
            {



                var notas = await dbContext.Notas.Include(n => n.Creador).Include(n => n.Usuarios_Liked).ThenInclude(l => l.Usuario).Include(l => l.Usuarios_DisLiked).ThenInclude(r => r.Usuario).FirstOrDefaultAsync(a => a.Id == notaid);

                if (notas is null)
                    return new Result<NotaResponse>() { Message = "la nota no existe", Success = false };

                var usuario = await dbContext.Usuarios.FirstOrDefaultAsync(u => u.Id == userid);
                if (usuario is null)
                    return new Result<NotaResponse>() { Message = "No estas logeado", Success = false };


                var existe = await dbContext.Usuario_Like_Notas.FirstOrDefaultAsync(r => r.Nota!.Id == notaid && r.Usuario!.Id == userid);

                if (existe is not null)
                {
                    notas.Cantidad_Likes -= 1;
                    dbContext.Usuario_Like_Notas.Remove(existe);
                }
                else
                {
                    var hay_dislike = await dbContext.Usuario_Dislike_Notas.FirstOrDefaultAsync(r => r.Nota!.Id == notaid && r.Usuario!.Id == userid);

                    if (hay_dislike is not null)
                    {
                        notas.Cantidad_Dislikes -= 1;
                        dbContext.Usuario_Dislike_Notas.Remove(hay_dislike);
                    }
                    var like = Usuario_Like_Nota.Crear(notas, usuario);

                    await dbContext.Usuario_Like_Notas.AddAsync(like);
                    notas.Cantidad_Likes += 1;
                }

                await dbContext.SaveChangesAsync();

                return new Result<NotaResponse>()
                {
                    Data = notas.ToResponse(),
                    Message = "Exito",
                    Success = true
                };





            }
            catch (Exception e)
            {
                return new Result<NotaResponse>()
                {

                    Message = e.InnerException?.Message ?? e.Message,
                    Success = false
                };
            }
        }

        public async Task<Result<NotaResponse>> DisLikeNota(int notaid, int userid)
        {

            try
            {



                var notas = await dbContext.Notas.Include(n => n.Creador).Include(n => n.Usuarios_Liked).ThenInclude(l => l.Usuario).Include(l => l.Usuarios_DisLiked).ThenInclude(r => r.Usuario).FirstOrDefaultAsync(a => a.Id == notaid);

                if (notas is null)
                    return new Result<NotaResponse>() { Message = "la nota no existe", Success = false };

                var usuario = await dbContext.Usuarios.FirstOrDefaultAsync(u => u.Id == userid);
                if (usuario is null)
                    return new Result<NotaResponse>() { Message = "No estas logeado", Success = false };

                var existe = await dbContext.Usuario_Dislike_Notas.FirstOrDefaultAsync(r => r.Nota!.Id == notaid && r.Usuario!.Id == userid);

                if (existe is not null)
                {
                    notas.Cantidad_Dislikes -= 1;
                    dbContext.Usuario_Dislike_Notas.Remove(existe);
                }
                else
                {
                    var hay_like = await dbContext.Usuario_Like_Notas.FirstOrDefaultAsync(r => r.Nota!.Id == notaid && r.Usuario!.Id == userid);

                    if (hay_like is not null)
                    {
                        notas.Cantidad_Likes -= 1;
                        dbContext.Usuario_Like_Notas.Remove(hay_like);
                    }
                    var dislike = Usuario_Dislike_Nota.Crear(notas, usuario);

                    await dbContext.Usuario_Dislike_Notas.AddAsync(dislike);
                    notas.Cantidad_Dislikes += 1;
                }



                await dbContext.SaveChangesAsync();

                return new Result<NotaResponse>()
                {
                    Data = notas.ToResponse(),
                    Message = "Exito",
                    Success = true
                };





            }
            catch (Exception e)
            {
                return new Result<NotaResponse>()
                {

                    Message = e.InnerException?.Message ?? e.Message,
                    Success = false
                };
            }
        }
    }
}
