using MetaMusic.Data.Entities;

namespace MetaMusic.Data.Responses
{
    public class ReviewResponse
    {
        public int Id { get; set; }

        public string Contenido { get; set; } = null!;

        public Usuario? Creador { get; set; } = new Usuario();
        public Album Album { get; set; } = new Album();
        public List<Reporte> Reportes { get; set; } = new List<Reporte>();


        public int IdAlbum { get; set; }
    }
}
