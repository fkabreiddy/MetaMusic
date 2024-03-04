using MetaMusic.Data.OtherEntities;
using MetaMusic.Data.Responses;

namespace MetaMusic.Data.Services
{
    public interface IPeticionService
    {
        Task<Result<bool>> CrearAlbum(string spotifyId);
        Task<Result<bool>> CrearArtista(string spotifyId);
        Task<Result<bool>> Eliminar(int id);

        Task<Result<List<PeticionResponse>>> ConsultarTodo();
    }
}