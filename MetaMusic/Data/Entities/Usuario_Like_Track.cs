using System.ComponentModel.DataAnnotations;

namespace MetaMusic.Data.Entities
{
    public class Usuario_Like_Track
    {
        [Key]
        public int Id { get; set; }

        public Usuario? Usuario { get; set; } = new Usuario();

        public Track? Track { get; set; } = new Track();
    }
}
