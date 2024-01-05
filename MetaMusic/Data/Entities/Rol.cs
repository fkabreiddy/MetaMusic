using MetaMusic.Data.Request;
using MetaMusic.Data.Responses;
using System.ComponentModel.DataAnnotations;

namespace MetaMusic.Data.Entities
{
    public class Rol
    {
        [Key]
        public int Id { get; set; }

        public string Tipo { get; set; } = "Normal";

        public List<Usuario> Usuarios { get; set; } = new List<Usuario>();

        public static Rol Crear(RolRequest request) => new Rol()
        {
            Id = request.Id,
            Tipo = request.Tipo,
            Usuarios = request.Usuarios
            // Agrega otras propiedades si es necesario
        };

        public bool Modificar(RolRequest rol)
        {
            bool modificacion = false;

            if (this.Tipo != rol.Tipo)
            {
                this.Tipo = rol.Tipo;
                modificacion = true;
            }

            if (!Usuarios.SequenceEqual(rol.Usuarios))
            {
                Usuarios = rol.Usuarios;
                modificacion = true;
            }

            // Agrega otras comparaciones y actualizaciones si es necesario

            return modificacion;
        }

        public RolResponse ToResponse()
        {
            return new RolResponse
            {
                Id = this.Id,
                Tipo = this.Tipo,
                Usuarios = this.Usuarios
                // Agrega otras asignaciones si es necesario
            };
        }
    }
}
