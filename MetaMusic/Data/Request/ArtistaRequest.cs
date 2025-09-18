using MetaMusic.Data.Entities;
using MetaMusic.Data.Responses;
using System.ComponentModel.DataAnnotations;

namespace MetaMusic.Data.Request
{
    public class ArtistaRequest
    {
        public int Id { get; set; }

        [Required]
        public string Foto_Perfil { get; set; } = null!;

        [Required]
        [StringLength(50)]
        public string Nombre { get; set; } = null!;

        [Required]
        [StringLength(50)]
        public string SpotifyId { get; set; } = null!;

        public List<Genero_Artista> GenerosMusicales { get; set; } = new List<Genero_Artista>();
        public List<Artista_Suscriptor> Suscriptores { get; set; } = new List<Artista_Suscriptor>();
        public Usuario? Creador { get; set; } = new Usuario();
        public List<Album_Artista> Albumes { get; set; } = new List<Album_Artista>();

        public Artista ToBaseClass() => new Artista()
        {
            Id = this.Id,
            Foto_Perfil = this.Foto_Perfil,
            Nombre = this.Nombre,
            SpotifyId = this.SpotifyId,
            GenerosMusicales = this.GenerosMusicales,
            Suscriptores = this.Suscriptores,
            Creador = this.Creador,
            Albumes = this.Albumes,
        };
    }
}
