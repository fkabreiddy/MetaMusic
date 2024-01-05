using MetaMusic.Data.Entities;

namespace MetaMusic.Data.Request
{
    public class SuscripcionRequest
    {
        public int Id { get; set; }

        public Usuario? Suscriptor { get; set; } = new Usuario();

        public Usuario? Usuario { get; set; } = new Usuario();
    }
}
