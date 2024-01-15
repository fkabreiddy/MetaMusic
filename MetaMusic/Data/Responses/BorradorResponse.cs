namespace MetaMusic.Data.Entities
{
    public class BorradorResponse
    {
        public int Id { get; set; }

        public DateTime Fecha { get; set; } = DateTime.Now;

        public Album Album { get; set; } = new Album();

        public Usuario Usuario { get; set; } = new Usuario();
    }
}
