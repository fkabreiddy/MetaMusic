using MetaMusic.Data.Entities;

namespace MetaMusic.Data.Responses
{
    public class GeneroResponse
    {
        public int Id { get; set; }

        public string Nombre { get; set; } = null!;

        public List<Genero_Artista> Artistas { get; set; } = new List<Genero_Artista>();

        public Genero ToBaseClass() => new Genero()
        {
            Id = this.Id,
            Nombre = this.Nombre,
            Artistas = this.Artistas
            // Agrega otras propiedades si es necesario
        };
    }
}
