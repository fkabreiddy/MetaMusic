using MetaMusic.Data.Request;
using MetaMusic.Data.Responses;
using System.ComponentModel.DataAnnotations;

namespace MetaMusic.Data.Entities
{
    public class Artista
    {
        [Key]
        public int Id { get; set; }

        
        public string Foto_Perfil { get; set; } = null!;

        [StringLength(250)]
        public string Nombre { get; set; } = null!;

        [StringLength(50)]
        public string SpotifyId { get; set; } = null!;

        
        public List<Genero_Artista> GenerosMusicales { get; set; } = new List<Genero_Artista>();
        public List<Artista_Suscriptor> Suscriptores { get; set; } = new List<Artista_Suscriptor>();
        public Usuario? Creador { get; set; } = new Usuario();
        public List<Album_Artista> Albumes { get; set; } = new List<Album_Artista>();

        public ArtistaResponse ToResponse() => new ArtistaResponse()
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
        public static Artista Crear(ArtistaRequest request) => new Artista()
        {
            Id = request.Id,
            Foto_Perfil = request.Foto_Perfil,
            Nombre = request.Nombre,
            SpotifyId = request.SpotifyId,
            GenerosMusicales = request.GenerosMusicales,
            Suscriptores = request.Suscriptores,
            Creador = request.Creador,
            Albumes = request.Albumes,
            // Agrega otras propiedades si es necesario
        };

        public bool Modificar(ArtistaRequest artista)
        {
            bool modificacion = false;

            if (this.Foto_Perfil != artista.Foto_Perfil)
            {
                this.Foto_Perfil = artista.Foto_Perfil;
                modificacion = true;
            }

            if (this.Nombre != artista.Nombre)
            {
                this.Nombre = artista.Nombre;
                modificacion = true;
            }
          
            if (this.SpotifyId != artista.SpotifyId)
            {
                this.SpotifyId = artista.SpotifyId;
                modificacion = true;
            }

            if (!GenerosMusicales.SequenceEqual(artista.GenerosMusicales))
            {
                GenerosMusicales = artista.GenerosMusicales;
                modificacion = true;
            }

            if (!Suscriptores.SequenceEqual(artista.Suscriptores))
            {
                Suscriptores = artista.Suscriptores;
                modificacion = true;
            }

            if (this.Creador != artista.Creador)
            {
                this.Creador = artista.Creador;
                modificacion = true;
            }

            if (!Albumes.SequenceEqual(artista.Albumes))
            {
                Albumes = artista.Albumes;
                modificacion = true;
            }

            // Agrega otras comparaciones y actualizaciones si es necesario

            return modificacion;
        }
    }
}
