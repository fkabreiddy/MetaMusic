using MetaMusic.Data.Request;
using MetaMusic.Data.Responses;

namespace MetaMusic.Data.Entities
{
    public class Peticion
    {
        public int Id { get; set; }
        public Usuario Usuario { get; set; } = new Usuario();

        public string? ArtistaNombre { get; set; }
        public string? AlbumNombre { get; set; }
        public string? ArtistaSpotifyId { get; set; }
        public string? AlbumSpotifyId { get; set; }

        public int Acumulaciones { get; set; } = 0;
        
        public DateTime UltimaPeticionFecha { get; set; }

        public PeticionResponse ToResponse() => new PeticionResponse()
        {
            Acumulaciones = this.Acumulaciones,
            Id = this.Id,
            Usuario = this.Usuario,
          
            AlbumNombre = this.AlbumNombre,
            ArtistaSpotifyId = this.ArtistaSpotifyId,
            AlbumSpotifyId = this.AlbumSpotifyId,
            ArtistaNombre = this.ArtistaNombre,
            UltimaPeticionFecha = this.UltimaPeticionFecha
        };
        public static Peticion Crear(PeticionRequest request) => new Peticion()
        {
            Id = request.Id,
            Usuario = request.Usuario,
            AlbumNombre = request.AlbumNombre,
            ArtistaSpotifyId = request.ArtistaSpotifyId,
            AlbumSpotifyId = request.AlbumSpotifyId,
            ArtistaNombre = request.ArtistaNombre,
            Acumulaciones = request.Acumulaciones,
            UltimaPeticionFecha = DateTime.Now,
           
        };

        public void Aumentar()
        {
            this.Acumulaciones += 1;
            this.UltimaPeticionFecha = DateTime.Now;
        }
    }
}
