using MetaMusic.Data.Entities;

namespace MetaMusic.Data.Request
{
    public class RolRequest
    {
        public int Id { get; set; }

        public string Tipo { get; set; } = "Normal";

        public List<Usuario> Usuarios { get; set; } = new List<Usuario>();
    }
}
