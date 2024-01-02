using System.ComponentModel.DataAnnotations;

namespace MetaMusic.Data.Entities
{
    public class Genero
    {
        [Key]
        public int Id { get; set; }

        public string Nombre { get; set; } = null!;

        public List<Genero_Artista> Artistas { get; set; } = new List<Genero_Artista>();
    }
}
