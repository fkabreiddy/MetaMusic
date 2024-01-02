using System.ComponentModel.DataAnnotations;

namespace MetaMusic.Data.Entities
{
    public class Album
    {
        [Key]
        public int Id { get; set; }

        public string Nombre { get; set; } = null!;

        public string IdSpotify { get; set; } = null!;

        public string Fecha_Publicacion = null!;

        public DateTime Fecha_Publicacion_Formateada;

        public DateTime Fecha_Agregado;

        public string Portada { get; set; } = null!;

        public List<Album_Artista> Artistas { get; set; } = new List<Album_Artista>();

        public Review Review { get; set; } = new Review();

        public Usuario? Creador { get; set; } = new Usuario();

        public List<Calificacion> Calificaciones { get; set; } = new List<Calificacion>();

        public List<Nota> Notas { get; set; } = new List<Nota>();
        public List<Track> Tracks { get; set; } = new List<Track>();

        public List<Notificacion> Notificaciones { get; set; } = new List<Notificacion>();
    }
}
