using MetaMusic.Data.Entities;

namespace MetaMusic.Data.Request
{
    public class ReporteRequest
    {
        public int Id { get; set; }

        public string Titulo { get; set; } = null!;

        public string Contenido { get; set; } = null!;

        public Usuario? Usuario { get; set; } = new Usuario();

        public Review? Review { get; set; } = new Review();
        public Nota? Nota { get; set; } = new Nota();

        public int Severidad { get; set; } = 0;
    }
}
