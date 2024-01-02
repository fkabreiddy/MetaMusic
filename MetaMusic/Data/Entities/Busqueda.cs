using System.ComponentModel.DataAnnotations;

namespace MetaMusic.Data.Entities
{
    public class Busqueda
    {
        [Key] public int Id { get; set; }

        public string Contenido { get; set; } = null!;

        public Usuario Usuario { get; set; } = new Usuario();
    }
}
