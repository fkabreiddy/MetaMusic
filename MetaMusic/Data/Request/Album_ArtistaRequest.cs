using MetaMusic.Data.Entities;

namespace MetaMusic.Data.Request
{
    public class Album_ArtistaRequest
    {
        public int Id { get; set; }

        public Artista? Artista { get; set; } = new Artista();

        public Album? Album { get; set; } = new Album();
    }
}
