using System.ComponentModel.DataAnnotations;

namespace MetaMusic.Data.Entities
{
    public class Usuario
    {

        [Key]
        public int Id { get; set; }

        public string Nombre { get; set; } = null!;
        public string Correo { get; set; } = null!;

        public string FotoDePerfil { get; set; } = null!;

        public string Biografia { get; set; } = null!;
        public Rol Rol { get; set; } = new Rol();

        public List<Artista> Artistas_Creados { get; set; } = new List<Artista>();

        public List<Album> Albumes_Publicados { get; set; } = new List<Album>();

        public List<Usuario_Dislike_Nota> Notas_DisLikeadas { get; set; } = new List<Usuario_Dislike_Nota>();
        public List<Usuario_Like_Nota> Notas_Likeadas { get; set; } = new List<Usuario_Like_Nota>();
        public List<Usuario_Like_Track> Liked_Tracks { get; set; } = new List<Usuario_Like_Track>(); 
        public List<Review> Reviews { get; set; } = new List<Review>();
        public List<Nota> Notas { get; set; } = new List<Nota>();
        public List<Notificacion> Notificaciones_Hechas { get; set; } = new List<Notificacion>();
        public List<Notificacion> Notificaciones_Recibidas { get; set; } = new List<Notificacion>();
        public List<Suscripcion> Suscriptores { get; set; } = new List<Suscripcion>();
        public List<Busqueda> Busquedas { get; set; } = new List<Busqueda>();
        public List<Suscripcion> Suscripciones { get; set; } = new List<Suscripcion>();
        public List<Reporte> Reportes { get; set; } = new List<Reporte>();

        public List<Calificacion> Calificaciones { get; set; } = new List<Calificacion>();


    }
}
