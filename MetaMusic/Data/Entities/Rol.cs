using System.ComponentModel.DataAnnotations;

namespace MetaMusic.Data.Entities
{
    public class Rol
    {
        [Key]
        public int Id { get; set; }

        public string Tipo { get; set; } = "Normal";

        public List<Usuario> Usuarios { get; set; } = new List<Usuario>();
    }
}
