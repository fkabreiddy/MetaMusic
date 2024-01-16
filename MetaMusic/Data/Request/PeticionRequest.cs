﻿using MetaMusic.Data.Entities;

namespace MetaMusic.Data.Request
{
    public class PeticionRequest
    {
        public int Id { get; set; }
        public Usuario Usuario { get; set; } = new Usuario();

        public string? ArtistaNombre { get; set; }
        public string? AlbumNombre { get; set; }
        public string? ArtistaSpotifyId { get; set; }
        public string? AlbumSpotifyId { get; set; }
        public int Acumulaciones { get; set; } = 0;

        public DateTime UltimaPeticionFecha { get; set; }
    }
}
