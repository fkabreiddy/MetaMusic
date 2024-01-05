using MetaMusic.Data.Entities;

namespace MetaMusic.Data.Responses
{
    public class BusquedaResponse
    {
        public int Id { get; set; }

        public string Contenido { get; set; } = null!;

        public Usuario Usuario { get; set; } = new Usuario();
    }
}
