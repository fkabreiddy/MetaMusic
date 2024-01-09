using MetaMusic.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace MetaMusic.Data.Context
{
    public class MetaMusicDbContext : DbContext, IMetaMusicDbContext
    {
        private readonly IConfiguration config;

        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Album> Albumes { get; set; }

        public DbSet<Artista> Artistas { get; set; }

        public DbSet<Busqueda> Busquedas { get; set; }

        public DbSet<Calificacion> Calificaciones { get; set; }

        public DbSet<Genero> Generos { get; set; }

        public DbSet<Genero_Artista> Genero_Artistas { get; set; }

        public DbSet<Nota> Notas { get; set; }

        public DbSet<Notificacion> Notificacions { get; set; }

        public DbSet<Artista_Suscriptor> Artista_Suscriptores { get; set; }
        public DbSet<Reporte> Reportes { get; set; }

        public DbSet<Review> Reviews { get; set; }

        public DbSet<Rol> Roles { get; set; }

        public DbSet<Suscripcion> Suscripciones { get; set; }

        public DbSet<Track> Tracks { get; set; }
        public DbSet<Peticion> Peticiones { get; set; }
        public DbSet<Usuario_Dislike_Nota> Usuario_Dislike_Notas { get; set; }

        public DbSet<Usuario_Like_Nota> Usuario_Like_Notas { get; set; }

        public DbSet<Usuario_Like_Track> Usuario_Like_Tracks { get; set; }
        public MetaMusicDbContext(IConfiguration config)
        {
            this.config = config;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(config.GetConnectionString("MSSQL"));

        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return base.SaveChangesAsync(cancellationToken);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Album>()
       .HasOne(a => a.Review)
       .WithOne(r => r.Album)
       .HasForeignKey<Review>(r => r.IdAlbum)
       .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<Suscripcion>().HasOne(s => s.Usuario).WithMany(u => u.Suscriptores).OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<Suscripcion>().HasOne(s => s.Suscriptor).WithMany(u => u.Suscripciones).OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Album_Artista>().HasOne(x => x.Artista).WithMany(a => a.Albumes).OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<Album_Artista>().HasOne(x => x.Album).WithMany(u => u.Artistas).OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Usuario_Dislike_Nota>().HasOne(x => x.Usuario).WithMany(u => u.Notas_DisLikeadas).OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<Usuario_Dislike_Nota>().HasOne(x => x.Nota).WithMany(u => u.Usuarios_DisLiked).OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Notificacion>().HasOne(b => b.UserFrom).WithMany(u => u.Notificaciones_Hechas).OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<Notificacion>().HasOne(b => b.UserTo).WithMany(u => u.Notificaciones_Recibidas).OnDelete(DeleteBehavior.NoAction);

            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

            modelBuilder.Entity<Notificacion>().HasOne(b => b.Album).WithMany(u => u.Notificaciones).OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Reporte>().HasOne(b => b.Usuario).WithMany(u => u.Reportes).OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<Reporte>().HasOne(b => b.Nota).WithMany(u => u.Reportes).OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<Reporte>().HasOne(b => b.Review).WithMany(u => u.Reportes).OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Usuario>().HasMany(b => b.Peticiones).WithOne(u => u.Usuario).OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<Artista>().HasMany(b => b.Peticiones).WithOne(u => u.Artista).OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Album>().HasMany(b => b.Peticiones).WithOne(u => u.Album).OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Usuario_Like_Nota>().HasOne(x => x.Usuario).WithMany(u => u.Notas_Likeadas).OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<Usuario_Like_Nota>().HasOne(x => x.Nota).WithMany(u => u.Usuarios_Liked).OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Usuario_Like_Track>().HasOne(x => x.Usuario).WithMany(u => u.Liked_Tracks).OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<Usuario_Like_Track>().HasOne(x => x.Track).WithMany(u => u.Usuarios_Liked).OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Busqueda>().HasOne(b => b.Usuario).WithMany(u => u.Busquedas).OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Calificacion>().HasOne(b => b.Usuario).WithMany(u => u.Calificaciones).OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Nota>().HasOne(b => b.Creador).WithMany(u => u.Notas).OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Nota>().HasOne(b => b.Album).WithMany(u => u.Notas).OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Usuario>().HasMany(b => b.Artistas_Creados).WithOne(u => u.Creador).OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<Usuario>().HasMany(b => b.Albumes_Publicados).WithOne(u => u.Creador).OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<Usuario>().HasMany(b => b.Albumes_Publicados).WithOne(u => u.Creador).OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<Usuario>().HasMany(b => b.Reviews).WithOne(u => u.Creador).OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<Usuario>().HasMany(b => b.Calificaciones).WithOne(u => u.Usuario).OnDelete(DeleteBehavior.NoAction);



            modelBuilder.Entity<Artista>().HasMany(a => a.GenerosMusicales).WithOne(g => g.Artista).OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<Nota>().HasOne(b => b.Album).WithMany(u => u.Notas).OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Review>().HasOne(b => b.Creador).WithMany(u => u.Reviews).OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Rol>().HasMany(b => b.Usuarios).WithOne(u => u.Rol).OnDelete(DeleteBehavior.NoAction);



        }

    }
}
