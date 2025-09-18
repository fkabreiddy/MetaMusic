using MetaMusic.Data.Request;
using SpotifyAPI.Web;
using System.ComponentModel.DataAnnotations;

namespace MetaMusic.Data.Entities
{
    public class Album_Artista
    {
        [Key]
        public int Id { get; set; }

        public Artista? Artista { get; set; } = new Artista();

        public Album? Album { get; set; } = new Album();

        public static Album_Artista Crear(Album_ArtistaRequest request) => new Album_Artista()
        {
            Id = request.Id,
            Artista = request.Artista,
            Album = request.Album
            // Agrega otras propiedades si es necesario
        };
        public bool Modificar(Album_ArtistaRequest artistaAlbum)
        {
            bool modificacion = false;

            if (this.Artista != artistaAlbum.Artista)
            {
                this.Artista = artistaAlbum.Artista;
                modificacion = true;
            }

            if (this.Album != artistaAlbum.Album)
            {
                this.Album = artistaAlbum.Album;
                modificacion = true;
            }

            

            return modificacion;
        }

    }
}
