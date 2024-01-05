﻿using MetaMusic.Data.Entities;

namespace MetaMusic.Data.Request
{
    public class NotificacionRequest
    {
        public int Id { get; set; }

        public string Titulo { get; set; } = null!;

        public Usuario UserTo { get; set; } = new Usuario();

        public Usuario? UserFrom { get; set; } = new Usuario();

        public Album? Album { get; set; } = new Album();
    }
}
