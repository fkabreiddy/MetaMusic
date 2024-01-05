using MetaMusic.Data.Entities;

namespace MetaMusic.Data.Responses
{
    public class Genero_ArtistaResponse
    {
        public int Id { get; set; }

        public Genero? Genero { get; set; } = new Genero();

        public Artista? Artista { get; set; } = new Artista();
    }
}
