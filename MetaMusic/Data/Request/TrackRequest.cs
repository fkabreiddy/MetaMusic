using MetaMusic.Data.Entities;

namespace MetaMusic.Data.Request
{
    public class TrackRequest
    {
        public int Id { get; set; }

        public string Titulo { get; set; } = null!;

        public Album Album { get; set; } = null!;



        public int Cantidad_Likes { get; set; } = 0;

        public List<Usuario_Like_Track> Usuarios_Liked { get; set; } = new List<Usuario_Like_Track>();
    }
}
