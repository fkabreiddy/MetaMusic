using MetaMusic.Data.Request;
using MetaMusic.Data.Responses;
using System.ComponentModel.DataAnnotations;

namespace MetaMusic.Data.Entities
{
    public class Notificacion
    {
        [Key]
        public int Id { get; set; }

        [StringLength(150)]
        public string Titulo { get; set; } = null!;

        public DateTime Fecha_Creacion { get; set; }
        public Usuario UserTo { get; set; } = new Usuario();

        public Usuario? UserFrom { get; set; } = new Usuario();

        public bool Saw { get; set; } = false;
        public Album? Album { get; set; } = new Album();
        public NotificacionResponse ToResponse()
        {
            return new NotificacionResponse
            {
                Id = this.Id,
                Titulo = this.Titulo,
                UserTo = this.UserTo,
                UserFrom = this.UserFrom,
                Album = this.Album,
                Fecha_Creacion = this.Fecha_Creacion,
                Saw = this.Saw
                
                // Agrega otras asignaciones si es necesario
            };
        }
        public static Notificacion Crear(NotificacionRequest request) => new Notificacion()
        {
            Id = request.Id,
            Titulo = request.Titulo,
            UserTo = request.UserTo,
            UserFrom = request.UserFrom,
            Album = request.Album,
            Fecha_Creacion = DateTime.Now,
            Saw = request.Saw
            
            // Agrega otras propiedades si es necesario
        };

        public bool Modificar(NotificacionRequest notificacion)
        {
            bool modificacion = false;

            if (this.Titulo != notificacion.Titulo)
            {
                this.Titulo = notificacion.Titulo;
                modificacion = true;
            }
            if (this.Saw != notificacion.Saw)
            {
                this.Saw = notificacion.Saw;
                modificacion = true;
            }


            if (this.UserTo != notificacion.UserTo)
            {
                this.UserTo = notificacion.UserTo;
                modificacion = true;
            }

            if (this.UserFrom != notificacion.UserFrom)
            {
                this.UserFrom = notificacion.UserFrom;
                modificacion = true;
            }

            if (this.Album != notificacion.Album)
            {
                this.Album = notificacion.Album;
                modificacion = true;
            }

            // Agrega otras comparaciones y actualizaciones si es necesario

            return modificacion;
        }

    }
}
