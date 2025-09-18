using MetaMusic.Data.Entities;

namespace MetaMusic.Data.Responses
{
    public class Artista_SuscriptorResponse
    {
        public int Id { get; set; }

        public Artista Artista { get; set; } = new Artista();

        public Usuario Usuario { get; set; } = new Usuario();
    }
}
