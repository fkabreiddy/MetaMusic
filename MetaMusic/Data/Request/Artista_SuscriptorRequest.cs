using MetaMusic.Data.Entities;

namespace MetaMusic.Data.Request
{
    public class Artista_SuscriptorRequest
    {
        public int Id { get; set; }

        public Artista Artista { get; set; } = new Artista();

        public Usuario Usuario { get; set; } = new Usuario();
    }
}
