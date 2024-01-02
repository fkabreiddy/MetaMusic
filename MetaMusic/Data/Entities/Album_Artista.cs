using System.ComponentModel.DataAnnotations;

namespace MetaMusic.Data.Entities
{
    public class Album_Artista
    {
        [Key]
        public int Id { get; set; }

        public Artista? Artista { get; set; } = new Artista();

        public Album? Album { get; set; } = new Album();
    }
}
