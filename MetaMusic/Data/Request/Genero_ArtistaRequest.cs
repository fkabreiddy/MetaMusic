using MetaMusic.Data.Entities;

namespace MetaMusic.Data.Request
{
    public class Genero_ArtistaRequest
    {
        public int Id { get; set; }

        public Genero? Genero { get; set; } = new Genero();

        public Artista? Artista { get; set; } = new Artista();
    }
}
