using MetaMusic.Data.Request;
using MetaMusic.Data.Responses;
using System.ComponentModel.DataAnnotations;

namespace MetaMusic.Data.Entities
{
    public class Track
    {
        [Key]
        public int Id { get; set; }


        [StringLength(250)]
        public string Titulo { get; set; } = null!;

        public Album Album { get; set; } = null!;

        public int Cantidad_Likes { get; set; } = 0;

        public List<Usuario_Like_Track> Usuarios_Liked { get; set; } = new List<Usuario_Like_Track>();

        public static Track Crear(TrackRequest request) => new Track()
        {
            Id = request.Id,
            Titulo = request.Titulo,
            Album = request.Album,

            Usuarios_Liked = request.Usuarios_Liked
            
            // Agrega otras propiedades si es necesario
        };

        public bool Modificar(TrackRequest trackResponse)
        {
            bool modificacion = false;

            if (this.Titulo != trackResponse.Titulo)
            {
                this.Titulo = trackResponse.Titulo;
                modificacion = true;
            }

            if (this.Album != trackResponse.Album)
            {
                this.Album = trackResponse.Album;
                modificacion = true;
            }

            

            if (!Usuarios_Liked.SequenceEqual(trackResponse.Usuarios_Liked))
            {
                Usuarios_Liked = trackResponse.Usuarios_Liked;
                modificacion = true;
            }

            // Agrega otras comparaciones y actualizaciones si es necesario

            return modificacion;
        }

        public TrackResponse ToResponse()
        {
            return new TrackResponse
            {
                Id = this.Id,
                Titulo = this.Titulo,
                Album = this.Album,
               
                Usuarios_Liked = this.Usuarios_Liked
                // Agrega otras asignaciones si es necesario
            };
        }

    }
}
