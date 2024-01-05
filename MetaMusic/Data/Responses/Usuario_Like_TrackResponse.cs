using MetaMusic.Data.Entities;
using MetaMusic.Data.Request;

namespace MetaMusic.Data.Responses
{
    public class Usuario_Like_TrackResponse
    {
        public int Id { get; set; }

        public Usuario? Usuario { get; set; } = new Usuario();

        public Track? Track { get; set; } = new Track();

       
    }
}
