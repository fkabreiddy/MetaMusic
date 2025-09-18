using MetaMusic.Data.Entities;
using MetaMusic.Data.Request;
using System.ComponentModel.DataAnnotations;

namespace MetaMusic.Data.Responses
{
    public class ArtistaResponse
    {
        public int Id { get; set; }
        public string Foto_Perfil { get; set; } = null!;
        public string Nombre { get; set; } = null!;
        public string SpotifyId { get; set; } = null!;

        public List<Genero_Artista> GenerosMusicales { get; set; } = new List<Genero_Artista>();
        public List<Artista_Suscriptor> Suscriptores { get; set; } = new List<Artista_Suscriptor>();
        public Usuario? Creador { get; set; } = new Usuario();
        public List<Album_Artista> Albumes { get; set; } = new List<Album_Artista>();


        public int GetAllTracksCount()
        {
            int total_tracks = 0;
           foreach (var relacion in Albumes)
            {
                if(relacion.Album is not null)
                     total_tracks += relacion.Album.Tracks.Count();
            }

            return total_tracks;

        }
        public ArtistaRequest ToRequest() => new ArtistaRequest()
        {
            Id = this.Id,
            Foto_Perfil = this.Foto_Perfil,
            Nombre = this.Nombre,
            SpotifyId = this.SpotifyId,
            GenerosMusicales = this.GenerosMusicales,
            Suscriptores = this.Suscriptores,
            Creador = this.Creador,
            Albumes = this.Albumes
        };
    }
}
