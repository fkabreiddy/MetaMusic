using MetaMusic.Data.Entities;

namespace MetaMusic.Data.Request
{
    public class UsuarioRequest
    {
        public int Id { get; set; }

        public string Nombre { get; set; } = null!;
        public string Correo { get; set; } = "";

        public string CorreoNormalizado { get; set; } = "";
        public string FotoDePerfil { get; set; } = "";

        public string Biografia { get; set; } = null!;
        public Rol Rol { get; set; } = new Rol();

        public List<Artista> Artistas_Creados { get; set; } = new List<Artista>();
        public List<Peticion> Peticiones { get; set; } = new List<Peticion>();
        public List<Album> Albumes_Publicados { get; set; } = new List<Album>();

        public List<Usuario_Dislike_Nota> Notas_DisLikeadas { get; set; } = new List<Usuario_Dislike_Nota>();
        public List<Usuario_Like_Nota> Notas_Likeadas { get; set; } = new List<Usuario_Like_Nota>();
        public List<Usuario_Like_Track> Liked_Tracks { get; set; } = new List<Usuario_Like_Track>();
        public List<Review> Reviews { get; set; } = new List<Review>();
        public List<Nota> Notas { get; set; } = new List<Nota>();
        public List<Notificacion> Notificaciones_Hechas { get; set; } = new List<Notificacion>();
        public List<Notificacion> Notificaciones_Recibidas { get; set; } = new List<Notificacion>();
      
        public List<Busqueda> Busquedas { get; set; } = new List<Busqueda>();

        public List<Reporte> Reportes { get; set; } = new List<Reporte>();
        public List<Artista_Suscriptor> Artistas_Suscritos { get; set; } = new List<Artista_Suscriptor>();
        public List<Calificacion> Calificaciones { get; set; } = new List<Calificacion>();



    }
}
