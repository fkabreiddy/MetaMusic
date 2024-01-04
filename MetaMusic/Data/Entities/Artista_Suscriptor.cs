using MetaMusic.Data.Context;
using System.ComponentModel.DataAnnotations;

namespace MetaMusic.Data.Entities
{
    public class Artista_Suscriptor
    {
        
        [Key] public int Id { get; set; }

        public Artista Artista { get; set; } = new Artista();

        public Usuario Usuario { get; set; } = new Usuario();

        
    }
}
