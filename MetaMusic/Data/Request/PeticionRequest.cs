using MetaMusic.Data.Entities;

namespace MetaMusic.Data.Request
{
    public class PeticionRequest
    {
        public int Id { get; set; }
        public Usuario Usuario { get; set; } = new Usuario();
        public Album? Album { get; set; } = new Album();
        public Artista Artista { get; set; } = new Artista();

        public int Acumulaciones { get; set; } = 0;

        public DateTime UltimaPeticionFecha { get; set; }
    }
}
