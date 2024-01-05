using MetaMusic.Data.Entities;

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
    }
}
