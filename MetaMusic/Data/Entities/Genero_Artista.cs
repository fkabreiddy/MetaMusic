using MetaMusic.Data.Request;
using MetaMusic.Data.Responses;
using System.ComponentModel.DataAnnotations;

namespace MetaMusic.Data.Entities
{
    public class Genero_Artista
    {
        [Key]
        public int Id { get; set; }

        public Genero? Genero { get; set; } = new Genero();

        public Artista? Artista { get; set; } = new Artista();

        public Genero_ArtistaResponse ToResponse() => new Genero_ArtistaResponse()
        {
            Id = this.Id,
            Genero = this.Genero,
            Artista = this.Artista
            // Agrega otras propiedades si es necesario
        };
        public static Genero_Artista Crear(Genero_ArtistaRequest request) => new Genero_Artista()
        {
            Id = request.Id,
            Genero = request.Genero,
            Artista = request.Artista
            // Agrega otras propiedades si es necesario
        };

        public bool Modificar(Genero_ArtistaRequest generoArtista)
        {
            bool modificacion = false;

            if (this.Genero != generoArtista.Genero)
            {
                this.Genero = generoArtista.Genero;
                modificacion = true;
            }

            if (this.Artista != generoArtista.Artista)
            {
                this.Artista = generoArtista.Artista;
                modificacion = true;
            }

            // Agrega otras comparaciones y actualizaciones si es necesario

            return modificacion;
        }
    }
}
