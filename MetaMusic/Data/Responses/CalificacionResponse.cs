using MetaMusic.Data.Entities;

namespace MetaMusic.Data.Responses
{
    public class CalificacionResponse
    {
        public int Id { get; set; }

        public Album Album { get; set; } = new Album();
        public Usuario? Usuario { get; set; } = new Usuario();


        public int Numero { get; set; }
    }
}
