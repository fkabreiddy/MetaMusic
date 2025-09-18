using MetaMusic.Data.Entities;
using MetaMusic.Data.Request;

namespace MetaMusic.Data.Responses
{
    public class TrackResponse
    {
        public int Id { get; set; }

        public string Titulo { get; set; } = null!;

        public Album Album { get; set; } = null!;

        public int Cantidad_Likes { get; set; } = 0;

        public List<Usuario_Like_Track> Usuarios_Liked { get; set; } = new List<Usuario_Like_Track>();


      
    }
}
