using System.ComponentModel.DataAnnotations;

namespace MetaMusic.Data.Entities
{
    public class Usuario_Like_Nota
    {
        [Key]
        public int Id { get; set; }

        public Usuario? Usuario { get; set; } = new Usuario();

        public Nota? Nota { get; set; } = new Nota();
    }
}
