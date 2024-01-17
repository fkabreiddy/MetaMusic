using MetaMusic.Data.Entities;
using System.ComponentModel.DataAnnotations;

namespace MetaMusic.Data.Request
{
    public class BusquedaRequest 
    {
        public int Id { get; set; }
  
        public string Contenido { get; set; } = null!;

        public Usuario? Usuario { get; set; } = new Usuario();
    }
}
