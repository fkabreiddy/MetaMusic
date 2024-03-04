using MetaMusic.Data.Entities;
using System.ComponentModel.DataAnnotations;

namespace MetaMusic.Data.Responses
{
    public class PeticionResponse
    {
        public int Id { get; set; }
        public Usuario Usuario { get; set; } = new Usuario();
        public string? ArtistaNombre { get; set; } 
        public string? AlbumNombre{ get; set; } 
        public string? ArtistaSpotifyId { get; set; } 
        public string? AlbumSpotifyId { get; set; } 
        public int Acumulaciones { get; set; } = 1;

        public string? ArtistaFoto { get; set; }


        public string? AlbumPortada { get; set; } 
        public DateTime UltimaPeticionFecha { get; set; }

        public bool isAlbum => AlbumSpotifyId != null;
    }
}
