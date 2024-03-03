using MetaMusic.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace MetaMusic.Data.Context
{
    public interface IMetaMusicDbContext
    {
        DbSet<Album> Albumes { get; set; }
        DbSet<Artista_Suscriptor> Artista_Suscriptores { get; set; }
        DbSet<Artista> Artistas { get; set; }
        DbSet<Busqueda> Busquedas { get; set; }
        DbSet<Calificacion> Calificaciones { get; set; }

      
        DbSet<Genero_Artista> Genero_Artistas { get; set; }
        DbSet<Genero> Generos { get; set; }
        DbSet<Nota> Notas { get; set; }
        DbSet<Notificacion> Notificacions { get; set; }
        DbSet<Reporte> Reportes { get; set; }
        DbSet<Review> Reviews { get; set; }
        DbSet<Rol> Roles { get; set; }
        DbSet<Peticion> Peticiones { get; set; }
        DbSet<Track> Tracks { get; set; }
        DbSet<Usuario_Dislike_Nota> Usuario_Dislike_Notas { get; set; }
        DbSet<Usuario_Like_Nota> Usuario_Like_Notas { get; set; }
        DbSet<Usuario_Like_Track> Usuario_Like_Tracks { get; set; }
        DbSet<Usuario> Usuarios { get; set; }
     
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}