using MetaMusic.Data.Entities;
using System.ComponentModel.DataAnnotations;

namespace MetaMusic.Data.Responses
{
    public class BusquedaResponse
    {
        public int Id { get; set; }

        public string Contenido { get; set; } = null!;

        public Usuario? Usuario { get; set; } = new Usuario();
    }
}
