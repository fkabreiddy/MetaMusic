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

        [StringLength(3000)]
        public string Contenido { get; set; } = null!;


        [StringLength(100)]
        public string Titulo { get; set; } = null!;
        public Usuario? Creador { get; set; } = new Usuario();

        [ForeignKey("Album")]
        public int AlbumId { get; set; }
        public Album Album { get; set; } = null!;
        public List<Reporte> Reportes { get; set; } = new List<Reporte>();

        public static Review Crear(ReviewRequest request) => new Review()
        {
            Id = request.Id,
            Contenido = request.Contenido,
            Creador = request.Creador,
            AlbumId = request.AlbumId,
            Reportes = request.Reportes,
           Album = request.Album,
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

          

            // Agrega otras comparaciones y actualizaciones si es necesario

            return modificacion;
        }

        public ReviewRequest ToRquest()
        {
            return new ReviewRequest
            {
                Id = this.Id,
                Contenido = this.Contenido,
                Creador = this.Creador,
                AlbumId = this.AlbumId,
                Reportes = this.Reportes,
                Album = this.Album,
                Titulo = this.Titulo
                // Agrega otras asignaciones si es necesario
            };
        }
        public ReviewResponse ToResponse()
        {
            return new ReviewResponse
            {
                Id = this.Id,
                Contenido = this.Contenido,
                Creador = this.Creador,
                AlbumId = this.AlbumId,
                Reportes = this.Reportes,
                Album = this.Album,
                Titulo = this.Titulo
                // Agrega otras asignaciones si es necesario
            };
        }

    }
}
