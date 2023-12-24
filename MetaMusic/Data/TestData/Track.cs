namespace MetaMusic.Data.TestData
{
    public class Track
    {

        public TimeSpan   Duracion{get; set;}
        public string Titulo { get; set; } = "";

        public int Calificacion;
        public Album Album { get; set; } = new Album();
    }
}
