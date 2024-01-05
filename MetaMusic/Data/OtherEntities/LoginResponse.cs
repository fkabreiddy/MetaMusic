using MetaMusic.Data.Entities;

namespace MetaMusic.Data.OtherEntities
{
    public class LoginResponse
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = "";

        public string Correo { get; set; } = "";
        public Rol Rol { get; set; } = new Rol();
    }
}
