using MetaMusic.Data.Entities;

namespace MetaMusic.Data.Responses
{
    public class NotaResponse
    {
        public int Id { get; set; }

        public string Contenido { get; set; } = string.Empty;

        public Usuario Creador { get; set; } = new Usuario();

        public DateTime Fecha_Creacion { get; set; }
        public Album Album { get; set; } = new Album();
        public int Likes => Usuarios_Liked.Count();

        public List<Usuario_Like_Nota> Usuarios_Liked = new List<Usuario_Like_Nota>();
        public int DisLikes => Usuarios_DisLiked.Count();

        public List<Usuario_Dislike_Nota> Usuarios_DisLiked = new List<Usuario_Dislike_Nota>();

        public List<Reporte> Reportes { get; set; } = new List<Reporte>();
    }
}
