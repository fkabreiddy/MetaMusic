using MetaMusic.Data.Entities;
using System.ComponentModel.DataAnnotations;

namespace MetaMusic.Data.Request
{
    public class NotaRequest
    {
        public int Id { get; set; }

        public string Contenido { get; set; } = null!;

        public Usuario? Creador { get; set; } = new Usuario();

        public int Cantidad_Dislikes { get; set; } = 0;

        public int Cantidad_Likes { get; set; } = 0;
        public Album? Album { get; set; } = new Album();


        public List<Usuario_Like_Nota> Usuarios_Liked = new List<Usuario_Like_Nota>();


        public List<Usuario_Dislike_Nota> Usuarios_DisLiked = new List<Usuario_Dislike_Nota>();

        public List<Reporte> Reportes { get; set; } = new List<Reporte>();
    }
}
