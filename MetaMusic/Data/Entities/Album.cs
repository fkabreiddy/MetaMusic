using MetaMusic.Data.Request;

using MetaMusic.Data.Responses;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace MetaMusic.Data.Entities
{
    public class Album
    {
        [Key]
        public int Id { get; set; }

        
        public bool Publicado { get; set; } = true;


        [StringLength(250)]
        public  string Nombre { get; set; } = null!;

        [StringLength(250)]
        public string IdSpotify { get; set; } = null!;

        [StringLength(25)]
        public string Fecha_Publicacion { get; set; } = null!;

        public bool Modificado { get; set; } = false;


        public DateTime? Fecha_Publicacion_Formateada { get; set; }


        public DateTime Fecha_Agregado { get; set; } 

        public DateTime? FechaModificado { get; set; }
        public string Portada { get; set; } = null!;

        public List<Album_Artista> Artistas { get; set; } = new List<Album_Artista>();


        public bool IsAlbumOfTheMonth { get; set; } = false;
        
        public Review? Review { get; set; } = new Review();

      
        public Usuario? Creador { get; set; } = new Usuario();

        public List<Calificacion> Calificaciones { get; set; } = new List<Calificacion>();

        
        
        
        public double Calificacion_Creador { get; set; } = 0.0;
        public List<Nota> Notas { get; set; } = new List<Nota>();
        public List<Track> Tracks { get; set; } = new List<Track>();

        public List<Notificacion> Notificaciones { get; set; } = new List<Notificacion>();


        //album al cual hace referencia este single (opcional)
        public int Reference { get; set; } = 0;

        public bool IsSingle { get; set; } = false;

        public bool IsMustListen { get; set; } = false;

      


        public static Album Crear(AlbumRequest request) => new Album()
        {
            Id = request.Id,
            Nombre = request.Nombre,
            IdSpotify = request.IdSpotify,
            Fecha_Publicacion = request.Fecha_Publicacion,
            Fecha_Publicacion_Formateada = request.Fecha_Publicacion_Formateada, // Asegúrate de manejar la conversión correctamente
            Fecha_Agregado = DateTime.Now, // Puedes establecer la fecha actual o manejarla según tus necesidades
            Portada = request.Portada,
            Artistas = request.Artistas,
            Review = request.Review,
            Creador = request.Creador,
            Calificaciones = request.Calificaciones,
            Notas = request.Notas,
            Tracks = request.Tracks,
            Notificaciones = request.Notificaciones,
            Calificacion_Creador = request.Calificacion_Creador,
            IsAlbumOfTheMonth = request.IsAlbumOfTheMonth,
            Publicado = true,
            Reference = request.Reference,
            IsMustListen = request.IsMustListen,
            IsSingle = request.IsSingle,
           


        };

        public AlbumResponse ToResponse() => new AlbumResponse()
        {
            Id = this.Id,
            Nombre = this.Nombre,
            IdSpotify = this.IdSpotify,
            Fecha_Publicacion = this.Fecha_Publicacion,
            Fecha_Publicacion_Formateada = this.Fecha_Publicacion_Formateada, // Asegúrate de manejar la conversión correctamente
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
            FechaModificado = this.FechaModificado,
            Publicado = this.Publicado,
            Modificado = this.Modificado,
            Reference = this.Reference,
            IsMustListen = this.IsMustListen,
            IsSingle = this.IsSingle,
          


        };

        public bool Modificar(AlbumRequest album)
        {
            bool modificacion = false;

            if (this.Nombre != album.Nombre)
            {
                Nombre = album.Nombre;
                modificacion = true;
            }
          
            if (this.IdSpotify != album.IdSpotify)
            {
                IdSpotify = album.IdSpotify;
                modificacion = true;
            }

            if (this.Publicado != album.Publicado)
            {
                Publicado = album.Publicado;
                modificacion = true;
            }
            if (this.Reference != album.Reference)
            {
                Reference = album.Reference;
                modificacion = true;
            }
            if (this.IsMustListen != album.IsMustListen)
            {
                IsMustListen = album.IsMustListen;
                modificacion = true;
            }
            if (this.IsSingle != album.IsSingle)
            {
                IsSingle = album.IsSingle;
                modificacion = true;
            }
            

            if (this.Calificacion_Creador != album.Calificacion_Creador)
            {
                Calificacion_Creador = album.Calificacion_Creador;
                modificacion = true;
            }

            if (this.IsAlbumOfTheMonth != album.IsAlbumOfTheMonth)
            {
                IsAlbumOfTheMonth = album.IsAlbumOfTheMonth;
                modificacion = true;
            }
            if (this.Fecha_Publicacion != album.Fecha_Publicacion)
            {
                Fecha_Publicacion = album.Fecha_Publicacion;
                modificacion = true;
            }

            if (this.Fecha_Publicacion_Formateada != album.Fecha_Publicacion_Formateada)
            {
                Fecha_Publicacion_Formateada = album.Fecha_Publicacion_Formateada;
                modificacion = true;
            }

            if (this.Fecha_Agregado != album.Fecha_Agregado)
            {
                Fecha_Agregado = album.Fecha_Agregado;
                modificacion = true;
            }

            if (this.Portada != album.Portada)
            {
                Portada = album.Portada;
                modificacion = true;
            }
          
            if (!Artistas.SequenceEqual(album.Artistas))
            {
                Artistas = album.Artistas;
                modificacion = true;
            }

            if (this.Review != album.Review)
            {
                Review = album.Review;
                modificacion = true;
            }

            if (this.Creador != album.Creador)
            {
                Creador = album.Creador;
                modificacion = true;
            }

            if (!Calificaciones.SequenceEqual(album.Calificaciones))
            {
                Calificaciones = album.Calificaciones;
                modificacion = true;
            }

            if (!Notas.SequenceEqual(album.Notas))
            {
                Notas = album.Notas;
                modificacion = true;
            }

            if (!Tracks.SequenceEqual(album.Tracks))
            {
                Tracks = album.Tracks;
                modificacion = true;
            }

            if (!Notificaciones.SequenceEqual(album.Notificaciones))
            {
                Notificaciones = album.Notificaciones;
                modificacion = true;
            }

         

            if(modificacion==true)
            {
                Modificado = modificacion;
                FechaModificado = DateTime.Now;

            }
            return modificacion;
        }
    }
}
