using MetaMusic.Data.Entities;

namespace MetaMusic.Data.Request
{
    public class Usuario_Dislike_NotaRequest
    {
        public int Id { get; set; }

        public Usuario? Usuario { get; set; } = new Usuario();

        public Nota? Nota { get; set; } = new Nota();
    }
}
