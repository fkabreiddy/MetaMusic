using MetaMusic.Data.Request;
using MetaMusic.Data.Responses;
using System.ComponentModel.DataAnnotations;

namespace MetaMusic.Data.Entities
{
    public class Genero
    {
        [Key]
        public int Id { get; set; }

        [StringLength(20)]
        public string Nombre { get; set; } = null!;

        public List<Genero_Artista> Artistas { get; set; } = new List<Genero_Artista>();

        public  GeneroResponse ToResponse() => new GeneroResponse()
        {
            Id = this.Id,
            Nombre = this.Nombre,
            Artistas = this.Artistas
            // Agrega otras propiedades si es necesario
        };

        public static Genero Crear(GeneroRequest request) => new Genero()
        {
            Id = request.Id,
            Nombre = request.Nombre,
            Artistas = request.Artistas
            // Agrega otras propiedades si es necesario
        };

        public bool Modificar(GeneroRequest genero)
        {
            bool modificacion = false;

            if (this.Nombre != genero.Nombre)
            {
                this.Nombre = genero.Nombre;
                modificacion = true;
            }

            if (!Artistas.SequenceEqual(genero.Artistas))
            {
                Artistas = genero.Artistas;
                modificacion = true;
            }

            // Agrega otras comparaciones y actualizaciones si es necesario

            return modificacion;
        }
    }
}
