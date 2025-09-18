using MetaMusic.Data.Entities;

namespace MetaMusic.Data.Request
{
    public class GeneroRequest
    {
        public int Id { get; set; }

        public string Nombre { get; set; } = null!;

        public List<Genero_Artista> Artistas { get; set; } = new List<Genero_Artista>();
    }
}
