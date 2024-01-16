using MetaMusic.Data.Entities;
using System.ComponentModel.DataAnnotations;

namespace MetaMusic.Data.Responses
{
    public class AlbumResponse 
    {

        public int Id { get; set; }


        public bool Publicado { get; set; } = true;


     
        public string Nombre { get; set; } = null!;

       
        public string IdSpotify { get; set; } = null!;

 
        public string Fecha_Publicacion { get; set; } = null!;

        public bool Modificado { get; set; } = false;


        public DateTime? Fecha_Publicacion_Formateada { get; set; }


        public DateTime Fecha_Agregado { get; set; }

        public DateTime? FechaModificado { get; set; }
        public string Portada { get; set; } = null!;

        public List<Album_Artista> Artistas { get; set; } = new List<Album_Artista>();

        public Review? Review { get; set; } = new Review();

        public Usuario? Creador { get; set; } = new Usuario();

        public List<Calificacion> Calificaciones { get; set; } = new List<Calificacion>();


        public double Calificacion_Creador { get; set; } = 0.0;
        public List<Nota> Notas { get; set; } = new List<Nota>();
        public List<Track> Tracks { get; set; } = new List<Track>();

        public List<Notificacion> Notificaciones { get; set; } = new List<Notificacion>();
    }
}
