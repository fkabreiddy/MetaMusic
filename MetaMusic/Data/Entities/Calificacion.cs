using System.ComponentModel.DataAnnotations;

namespace MetaMusic.Data.Entities
{
    public class Calificacion
    {
        [Key]
        public int Id { get; set; }

        public Album Album { get; set; }= new Album();
        public Usuario? Usuario { get; set; } = new Usuario();


        public int Numero { get; set; }
    }
}
