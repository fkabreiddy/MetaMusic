using MetaMusic.Data.Entities;

namespace MetaMusic.Data.Request
{
    public class BusquedaRequest 
    {
        public int Id { get; set; }

        public string Contenido { get; set; } = null!;

        public Usuario Usuario { get; set; } = new Usuario();
    }
}
