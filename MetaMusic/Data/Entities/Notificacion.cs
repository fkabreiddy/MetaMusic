using System.ComponentModel.DataAnnotations;

namespace MetaMusic.Data.Entities
{
    public class Notificacion
    {
        [Key]
        public int Id { get; set; }

        public string Titulo { get; set; } = null!;

        public Usuario UserTo { get; set; } = new Usuario();

        public Usuario? UserFrom { get; set; } = new Usuario();

        public Album? Album { get; set; } = new Album();

    }
}
