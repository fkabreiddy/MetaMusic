using MetaMusic.Data.Entities;

namespace MetaMusic.Data.Request
{
    public class Usuario_Like_TrackRequest
    {
        public int Id { get; set; }

        public Usuario? Usuario { get; set; } = new Usuario();

        public Track? Track { get; set; } = new Track();
    }
}
