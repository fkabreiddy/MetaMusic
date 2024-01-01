namespace MetaMusic.Data.TestData
{
    public class Peticion
    {

        public DateTime fecha = DateTime.Now;

        public Album Album { get; set; } = new Album();

        public Artista Artista = new Artista();
    }
}
