using System.ComponentModel.DataAnnotations;

namespace MetaMusic.Data.Entities
{
    public class Reporte
    {
        [Key]
        public int Id { get; set; }

        public string Titulo { get; set; } = null!;

        public string Contenido { get; set; } = null!;

        public Usuario Usuario { get; set; } = new Usuario();

        public Review? Review { get; set; } = new Review();
        public Nota? Nota { get; set; } = new Nota();
    }
}
