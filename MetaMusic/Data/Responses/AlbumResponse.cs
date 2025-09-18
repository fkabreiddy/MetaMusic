using MetaMusic.Data.Entities;
using MetaMusic.Data.Request;
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

        public double Promedio_Calificaciones => Calificaciones.Count() >= 1 ? Math.Round(Calificaciones.Average(c => c.Numero), 1) : 0;
        public double Calificacion_Creador { get; set; } = 0.0;
        public List<Nota> Notas { get; set; } = new List<Nota>();
        public List<Track> Tracks { get; set; } = new List<Track>();

        public List<Notificacion> Notificaciones { get; set; } = new List<Notificacion>();
        public bool IsAlbumOfTheMonth { get; set; } = false;

        public int Reference { get; set; }

        public bool IsSingle { get; set; }

        public bool IsMustListen { get; set; }
        public AlbumRequest ToRequest() => new AlbumRequest()
        {
            Id = this.Id,
            Nombre = this.Nombre,
            IdSpotify = this.IdSpotify,
            Fecha_Publicacion = this.Fecha_Publicacion,

            Fecha_Agregado = this.Fecha_Agregado, // Puedes establecer la fecha actual o manejarla según tus necesidades
            Portada = this.Portada,
            Artistas = this.Artistas,
            Review = this.Review,
            Creador = this.Creador,
            Calificaciones = this.Calificaciones,
            Notas = this.Notas,
            Tracks = this.Tracks,
            Notificaciones = this.Notificaciones,
            Calificacion_Creador = this.Calificacion_Creador,
            IsAlbumOfTheMonth = this.IsAlbumOfTheMonth,
            Reference = this.Reference,
            IsMustListen = this.IsMustListen,
            IsSingle = this.IsSingle,
            
            Publicado = this.Publicado

        };
    }
}
