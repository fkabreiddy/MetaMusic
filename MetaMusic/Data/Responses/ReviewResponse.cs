using MetaMusic.Data.Entities;
using MetaMusic.Data.Request;

namespace MetaMusic.Data.Responses
{
    public class ReviewResponse
    {
        public int Id { get; set; }

        public string Contenido { get; set; } = null!;
        public string Titulo { get; set; } = null!;
        public Usuario? Creador { get; set; } = new Usuario();
        public Album Album { get; set; } = new Album();
        public List<Reporte> Reportes { get; set; } = new List<Reporte>();
        public int AlbumId { get; set; }

        public ReviewRequest ToRequest()
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

    }
}
