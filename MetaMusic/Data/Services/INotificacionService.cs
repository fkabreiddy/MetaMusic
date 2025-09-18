using MetaMusic.Data.OtherEntities;
using MetaMusic.Data.Responses;

namespace MetaMusic.Data.Services
{
    public interface INotificacionService
    {
        Task<Result<bool>> NotificarNuevoAlbum(int _albumid);

        Task<Result<bool>> NotificacionGenerica(int _usertoId, string title);

        Task<Result<List<NotificacionResponse>>> Consultar();

        Task<Result<bool>> NotificarReviewOculta(int _albumid);

        Task<Result<bool>> Eliminar(int notificationId);

        Task<Result<bool>> MarcarComoLeida(List<int> notificacions);
    }
}