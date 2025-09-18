using MetaMusic.Data.Entities;

namespace MetaMusic.Data.Request
{
    public class CalificacionRequest
    {
        public int Id { get; set; }

        public Album? Album { get; set; } = new Album();
        public Usuario? Usuario { get; set; } = new Usuario();


        public double Numero { get; set; } = 0.0;
    }
}
