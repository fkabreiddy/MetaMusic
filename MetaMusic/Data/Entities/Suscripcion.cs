using System.ComponentModel.DataAnnotations;

namespace MetaMusic.Data.Entities
{
    public class Suscripcion
    {
        [Key]
        public int Id { get; set; }

        public Usuario? Suscriptor { get; set; } = new Usuario();

        public Usuario? Usuario { get; set; } = new Usuario();
    }
}
