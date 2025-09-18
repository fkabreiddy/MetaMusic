using MetaMusic.Data.Request;
using MetaMusic.Data.Responses;
using System.ComponentModel.DataAnnotations;

namespace MetaMusic.Data.Entities
{
    public class Busqueda
    {
        [Key] public int Id { get; set; }



        [StringLength(50)]
        public string Contenido { get; set; } = null!;

        public  Usuario? Usuario { get; set; } = new Usuario();

        public static Busqueda Crear(BusquedaRequest request) => new Busqueda()
        {
            Id = request.Id,
            Contenido = request.Contenido,
            Usuario = request.Usuario
            // Agrega otras propiedades si es necesario
        };

        public BusquedaResponse ToResponse() => new BusquedaResponse() {

            Id = this.Id,
            Contenido = this.Contenido,
            Usuario = this.Usuario

        };

        public bool Modificar(BusquedaRequest busqueda)
        {
            bool modificacion = false;

            if (this.Contenido != busqueda.Contenido)
            {
                this.Contenido = busqueda.Contenido;
                modificacion = true;
            }

            if (this.Usuario != busqueda.Usuario)
            {
                this.Usuario = busqueda.Usuario;
                modificacion = true;
            }

            // Agrega otras comparaciones y actualizaciones si es necesario

            return modificacion;
        }
    }
}
