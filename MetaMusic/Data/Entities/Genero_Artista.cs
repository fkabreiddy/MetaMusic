using System.ComponentModel.DataAnnotations;

namespace MetaMusic.Data.Entities
{
    public class Genero_Artista
    {
        [Key]
        public int Id { get; set; }

        public Genero? Genero { get; set; } = new Genero();

        public Artista? Artista { get; set; } = new Artista();
    }
}
