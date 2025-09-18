using MetaMusic.Data.Entities;

namespace MetaMusic.Data.OtherEntities
{
    public class LoginResponse
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = "";

        public string Correo { get; set; } = "";

        public string JWT { get; set; } = "";
        public Rol Rol { get; set; } = new Rol();

        
        private void GetEmailForJWT(string email)
        {
            this.Correo = email;
        }
    }
}
