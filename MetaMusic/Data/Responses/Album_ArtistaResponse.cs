using MetaMusic.Data.Entities;

namespace MetaMusic.Data.Responses
{
    public class Album_ArtistaResponse
    {
        public int Id { get; set; }

        public Artista? Artista { get; set; } = new Artista();

        public Album? Album { get; set; } = new Album();
    }
}
