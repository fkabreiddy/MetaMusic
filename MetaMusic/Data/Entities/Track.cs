using System.ComponentModel.DataAnnotations;

namespace MetaMusic.Data.Entities
{
    public class Track
    {
        [Key]
        public int Id { get; set; }

        public string Titulo { get; set; } = null!;

        public Album Album { get; set; } = null!;

        public TimeSpan Duracion = new TimeSpan();

        public int Likes => Usuarios_Liked.Count();

        public List<Usuario_Like_Track> Usuarios_Liked { get; set; } = new List<Usuario_Like_Track>();



    }
}
