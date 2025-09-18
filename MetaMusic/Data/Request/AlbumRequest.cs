
using MetaMusic.Data.Entities;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;

namespace MetaMusic.Data.Request
{
    public class AlbumRequest 
    {

        public int Id { get; set; }


        public bool Publicado { get; set; } = true;


        [Required]
        [StringLength(50)]
        public string Nombre { get; set; } = null!;

        [Required]
        [StringLength(50)]
        public string IdSpotify { get; set; } = null!;

       
        [StringLength(25)]
        public string Fecha_Publicacion { get; set; } = null!;

        public bool Modificado { get; set; } = false;

        public DateTime Fecha_Agregado { get; set; }
        public  DateTime? Fecha_Publicacion_Formateada => DateTime.ParseExact(this.Fecha_Publicacion, "yyyy-MM-dd", CultureInfo.InvariantCulture);

       

        [Required]
        public string Portada { get; set; } = null!;

        public List<Album_Artista> Artistas { get; set; } = new List<Album_Artista>();

        public Review? Review { get; set; } = new Review();

        public Usuario? Creador { get; set; } = new Usuario();

        public List<Calificacion> Calificaciones { get; set; } = new List<Calificacion>();

   
        public int Reference { get; set; }

        public bool IsSingle { get; set; }

        public bool IsMustListen { get; set; }
        public bool IsAlbumOfTheMonth { get; set; } = false;
        public double Calificacion_Creador { get; set; } = 0.0;
        public List<Nota> Notas { get; set; } = new List<Nota>();
        public List<Track> Tracks { get; set; } = new List<Track>();

        public List<Notificacion> Notificaciones { get; set; } = new List<Notificacion>();


    }
}
