using MetaMusic.Data.Request;
using MetaMusic.Data.Responses;
using System.ComponentModel.DataAnnotations;

namespace MetaMusic.Data.Entities
{
    public class Suscripcion
    {
        [Key]
        public int Id { get; set; }

        public Usuario? Suscriptor { get; set; } = new Usuario();

        public Usuario? Usuario { get; set; } = new Usuario();



        public static Suscripcion Crear(SuscripcionRequest request) => new Suscripcion()
        {
            Id = request.Id,
            Suscriptor = request.Suscriptor,
            Usuario = request.Usuario
            // Agrega otras propiedades si es necesario
        };

        public bool Modificar(SuscripcionRequest suscripcion)
        {
            bool modificacion = false;

            if (this.Suscriptor != suscripcion.Suscriptor)
            {
                this.Suscriptor = suscripcion.Suscriptor;
                modificacion = true;
            }

            if (this.Usuario != suscripcion.Usuario)
            {
                this.Usuario = suscripcion.Usuario;
                modificacion = true;
            }

            // Agrega otras comparaciones y actualizaciones si es necesario

            return modificacion;
        }

        public SuscripcionResponse ToResponse()
        {
            return new SuscripcionResponse
            {
                Id = this.Id,
                Suscriptor = this.Suscriptor,
                Usuario = this.Usuario
                // Agrega otras asignaciones si es necesario
            };
        }
    }



}
