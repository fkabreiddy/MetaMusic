using MetaMusic.Data.Request;
using MetaMusic.Data.Responses;
using System.ComponentModel.DataAnnotations;

namespace MetaMusic.Data.Entities
{
    public class Calificacion
    {
        [Key]
        public int Id { get; set; }

        public Album? Album { get; set; }= new Album();
        public Usuario? Usuario { get; set; } = new Usuario();


        public double Numero { get; set; } = 0.0;

        public static Calificacion Crear(CalificacionRequest request) => new Calificacion()
        {
            Id = request.Id,
            Album = request.Album,
            Usuario = request.Usuario,
            Numero = request.Numero
            // Agrega otras propiedades si es necesario
        };

        public  CalificacionResponse ToResponse() => new CalificacionResponse()
        {
            Id = this.Id,
            Album = this.Album,
            Usuario = this.Usuario,
            Numero = this.Numero
            // Agrega otras propiedades si es necesario
        };
        public bool Modificar(CalificacionRequest calificacion)
        {
            bool modificacion = false;

            if (this.Album != calificacion.Album)
            {
                this.Album = calificacion.Album;
                modificacion = true;
            }

            if (this.Usuario != calificacion.Usuario)
            {
                this.Usuario = calificacion.Usuario;
                modificacion = true;
            }

            if (this.Numero != calificacion.Numero)
            {
                this.Numero = calificacion.Numero;
                modificacion = true;
            }

            // Agrega otras comparaciones y actualizaciones si es necesario

            return modificacion;
        }
    }
}
