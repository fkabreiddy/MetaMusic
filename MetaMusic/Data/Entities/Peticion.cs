using MetaMusic.Data.Request;
using MetaMusic.Data.Responses;
using System.ComponentModel.DataAnnotations;

namespace MetaMusic.Data.Entities
{
    public class Peticion
    {
        public int Id { get; set; }
        public Usuario Usuario { get; set; } = new Usuario();

        [StringLength(250)]
        public string? ArtistaNombre { get; set; }

        [StringLength(250)]
        public string? AlbumNombre { get; set; }

        [StringLength(50)]
        public string? ArtistaSpotifyId { get; set; }

        [StringLength(50)]
        public string? AlbumSpotifyId { get; set; }

        
        public string? ArtistaFoto { get; set; }

    
        public string? AlbumPortada { get; set; }
        
        
        public DateTime UltimaPeticionFecha { get; set; }

        public PeticionResponse ToResponse() => new PeticionResponse()
        {
           
            Id = this.Id,
            Usuario = this.Usuario,
          
            AlbumNombre = this.AlbumNombre,
            ArtistaSpotifyId = this.ArtistaSpotifyId,
            AlbumSpotifyId = this.AlbumSpotifyId,
            ArtistaNombre = this.ArtistaNombre,
            UltimaPeticionFecha = this.UltimaPeticionFecha,
            AlbumPortada = this.AlbumPortada,
            ArtistaFoto = this.ArtistaFoto,
        };
        public static Peticion Crear(PeticionRequest request) => new Peticion()
        {
            Id = request.Id,
            Usuario = request.Usuario,
            AlbumNombre = request.AlbumNombre,
            ArtistaSpotifyId = request.ArtistaSpotifyId,
            AlbumSpotifyId = request.AlbumSpotifyId,
            ArtistaNombre = request.ArtistaNombre,
           
            UltimaPeticionFecha = DateTime.Now,
            AlbumPortada = request.AlbumPortada,
            ArtistaFoto = request.ArtistaFoto,
        };

        
    }
}
