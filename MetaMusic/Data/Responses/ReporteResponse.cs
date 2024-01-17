using MetaMusic.Data.Entities;

namespace MetaMusic.Data.Responses
{
    public class ReporteResponse
    {
        public int Id { get; set; }

        public string Titulo { get; set; } = null!;

        public string Contenido { get; set; } = null!;

        public DateTime Fecha_Creacion { get; set; }
        public Usuario? Usuario { get; set; } = new Usuario();

        public Review? Review { get; set; } = new Review();
        public Nota? Nota { get; set; } = new Nota();
    }
}
