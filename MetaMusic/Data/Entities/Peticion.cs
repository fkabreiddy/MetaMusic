using MetaMusic.Data.Request;
using MetaMusic.Data.Responses;

namespace MetaMusic.Data.Entities
{
    public class Peticion
    {
        public int Id { get; set; }
        public Usuario Usuario { get; set; } = new Usuario();
        public Album? Album { get; set; } = new Album();
        public Artista Artista { get; set; } = new Artista();

        public int Acumulaciones { get; set; } = 0;
        
        public DateTime UltimaPeticionFecha { get; set; }

        public PeticionResponse ToResponse() => new PeticionResponse()
        {
            Acumulaciones = this.Acumulaciones,
            Id = this.Id,
            Usuario = this.Usuario,
            Album = this.Album,
            Artista = this.Artista,
            UltimaPeticionFecha = this.UltimaPeticionFecha
        };
        public static Peticion Crear(PeticionRequest request) => new Peticion()
        {
            Id = request.Id,
            Usuario = request.Usuario,
            Album = request.Album,
            Acumulaciones = request.Acumulaciones,
            UltimaPeticionFecha = DateTime.Now,
            Artista = request.Artista
        };

        public void Aumentar()
        {
            this.Acumulaciones += 1;
            this.UltimaPeticionFecha = DateTime.Now;
        }
    }
}
