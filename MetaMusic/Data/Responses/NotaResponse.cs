using MetaMusic.Data.Entities;
using System.ComponentModel.DataAnnotations;

namespace MetaMusic.Data.Responses
{
    public class NotaResponse
    {
        public int Id { get; set; }


        [StringLength(100)]
        public string Contenido { get; set; } = "";

        public Usuario? Creador { get; set; } = new Usuario();

        public DateTime Fecha_Creacion { get; set; }
        public Album? Album { get; set; } = new Album();

        public int Cantidad_Dislikes { get; set; } = 0;

        public int Cantidad_Likes { get; set; } = 0;

        public List<Usuario_Like_Nota> Usuarios_Liked = new List<Usuario_Like_Nota>();


        public List<Usuario_Dislike_Nota> Usuarios_DisLiked = new List<Usuario_Dislike_Nota>();

        public List<Reporte> Reportes { get; set; } = new List<Reporte>();

        
       
    }
}
