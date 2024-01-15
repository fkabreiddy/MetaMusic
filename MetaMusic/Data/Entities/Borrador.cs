using System.ComponentModel.DataAnnotations;
using MetaMusic.Data.Request;
namespace MetaMusic.Data.Entities
{
    public class Borrador
    {
        [Key]
        public int Id { get; set; }

        public DateTime Fecha { get; set; } = DateTime.Now;

        public Album Album { get; set; } = new Album();

        public Usuario Usuario { get; set; } = new Usuario();
        public BorradorResponse ToResponse()
            => new BorradorResponse()
            {
                Id = this.Id,
                Fecha = this.Fecha,
                Album = this.Album,
                Usuario = this.Usuario,
            };

        public static Borrador Crear(BorradorRequest request) => new()
        {
            Id = request.Id,
            Fecha = request.Fecha,
            Album = request.Album,
            Usuario = request.Usuario,
        };

        public bool Modificar(BorradorRequest request)
        {
            bool modificacion = false;

            

            if (this.Album != request.Album)
            {
                Album = request.Album;
                modificacion = true;
            }

            return modificacion;
        }
    }
}
