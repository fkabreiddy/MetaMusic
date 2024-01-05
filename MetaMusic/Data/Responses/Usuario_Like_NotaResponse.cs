using MetaMusic.Data.Entities;
using MetaMusic.Data.Request;

namespace MetaMusic.Data.Responses
{
    public class Usuario_Like_NotaResponse
    {
        public int Id { get; set; }

        public Usuario? Usuario { get; set; } = new Usuario();

        public Nota? Nota { get; set; } = new Nota();

       
    }
}
