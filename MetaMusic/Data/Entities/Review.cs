using MetaMusic.Data.TestData;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MetaMusic.Data.Entities
{
    public class Review
    {
        [Key]
        public int Id { get; set; }

        public string Contenido { get; set; } = null!;

        public Usuario? Creador { get; set; } = new Usuario();
        public Album Album { get; set; } = new Album();
        public List<Reporte> Reportes { get; set; } = new List<Reporte>();

      
        public int IdAlbum { get; set; }

        
    }
}
