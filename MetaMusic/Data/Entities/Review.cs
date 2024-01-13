using MetaMusic.Data.Request;
using MetaMusic.Data.Responses;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MetaMusic.Data.Entities
{
    public class Review
    {
        [Key]
        public int Id { get; set; }

        public string Contenido { get; set; } = null!;

        public string Titulo { get; set; } = null!;
        public Usuario? Creador { get; set; } = new Usuario();
        public Album? Album { get; set; } 
        public List<Reporte> Reportes { get; set; } = new List<Reporte>();

      
        public int IdAlbum { get; set; }

        public static Review Crear(ReviewRequest request) => new Review()
        {
            Id = request.Id,
            Contenido = request.Contenido,
            Creador = request.Creador,
            Album = request.Album,
            Reportes = request.Reportes,
            IdAlbum = request.IdAlbum,
            Titulo = request.Titulo
            // Agrega otras propiedades si es necesario
        };

        public bool Modificar(ReviewRequest review)
        {
            bool modificacion = false;

            if (this.Contenido != review.Contenido)
            {
                this.Contenido = review.Contenido;
                modificacion = true;
            }

            if (this.Titulo != review.Titulo)
            {
                this.Titulo = review.Titulo;
                modificacion = true;
            }

            if (this.Creador != review.Creador)
            {
                this.Creador = review.Creador;
                modificacion = true;
            }

            if (this.Album != review.Album)
            {
                this.Album = review.Album;
                modificacion = true;
            }

            if (!Reportes.SequenceEqual(review.Reportes))
            {
                Reportes = review.Reportes;
                modificacion = true;
            }

            if (this.IdAlbum != review.IdAlbum)
            {
                this.IdAlbum = review.IdAlbum;
                modificacion = true;
            }

            // Agrega otras comparaciones y actualizaciones si es necesario

            return modificacion;
        }

        public ReviewResponse ToResponse()
        {
            return new ReviewResponse
            {
                Id = this.Id,
                Contenido = this.Contenido,
                Creador = this.Creador,
                Album = this.Album,
                Reportes = this.Reportes,
                IdAlbum = this.IdAlbum,
                Titulo = this.Titulo
                // Agrega otras asignaciones si es necesario
            };
        }

    }
}
