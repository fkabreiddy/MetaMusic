using MetaMusic.Data.Request;
using MetaMusic.Data.Responses;
using System.ComponentModel.DataAnnotations;

namespace MetaMusic.Data.Entities
{
    public class Usuario_Like_Track
    {
        [Key]
        public int Id { get; set; }

        public Usuario? Usuario { get; set; } = new Usuario();

        public Track? Track { get; set; } = new Track();

        public static Usuario_Like_Track Crear(Usuario_Like_TrackRequest request) => new Usuario_Like_Track()
        {
            Id = request.Id,
            Usuario = request.Usuario,
            Track = request.Track
            // Agrega otras propiedades si es necesario
        };

        public bool Modificar(Usuario_Like_TrackRequest usuarioLikeTrackResponse)
        {
            bool modificacion = false;

            if (this.Usuario != usuarioLikeTrackResponse.Usuario)
            {
                this.Usuario = usuarioLikeTrackResponse.Usuario;
                modificacion = true;
            }

            if (this.Track != usuarioLikeTrackResponse.Track)
            {
                this.Track = usuarioLikeTrackResponse.Track;
                modificacion = true;
            }

            // Agrega otras comparaciones y actualizaciones si es necesario

            return modificacion;
        }

        public Usuario_Like_TrackResponse ToResponse()
        {
            return new Usuario_Like_TrackResponse
            {
                Id = this.Id,
                Usuario = this.Usuario,
                Track = this.Track
                // Agrega otras asignaciones si es necesario
            };
        }
    }
}
