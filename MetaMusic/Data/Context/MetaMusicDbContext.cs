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

            //Usuario 

            modelBuilder.Entity<Usuario>().HasMany(u => u.Artistas_Creados).WithOne(a => a.Creador).OnDelete(DeleteBehavior.SetNull) ;
            modelBuilder.Entity<Usuario>().HasMany(u => u.Albumes_Publicados).WithOne(a => a.Creador).OnDelete(DeleteBehavior.SetNull);
            modelBuilder.Entity<Usuario>().HasMany(u => u.Liked_Tracks).WithOne(a => a.Usuario).OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<Usuario>().HasMany(u => u.Notas_DisLikeadas).WithOne(a => a.Usuario).OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<Usuario>().HasMany(u => u.Notas_Likeadas).WithOne(a => a.Usuario).OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<Usuario>().HasMany(u => u.Notificaciones_Recibidas).WithOne(a => a.UserTo).OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<Usuario>().HasMany(u => u.Notificaciones_Hechas).WithOne(a => a.UserFrom).OnDelete(DeleteBehavior.SetNull);
            modelBuilder.Entity<Usuario>().HasMany(u => u.Busquedas).WithOne(a => a.Usuario).OnDelete(DeleteBehavior.SetNull);
            modelBuilder.Entity<Usuario>().HasMany(u => u.Reportes).WithOne(a => a.Usuario).OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<Usuario>().HasMany(u => u.Artistas_Suscritos).WithOne(a => a.Usuario).OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<Usuario>().HasMany(u => u.Calificaciones).WithOne(a => a.Usuario).OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<Usuario>().HasMany(u => u.Peticiones).WithOne(a => a.Usuario).OnDelete(DeleteBehavior.Cascade);
       
            //albumes

            modelBuilder.Entity<Album>().HasMany(a => a.Artistas).WithOne(r => r.Album).OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<Album>().HasMany(a => a.Calificaciones).WithOne(r => r.Album).OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<Album>().HasOne(a => a.Review).WithOne(r => r.Album).HasForeignKey<Review>(r => r.AlbumId).IsRequired(true).OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Album>().HasMany(a => a.Notas).WithOne(r => r.Album).OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<Album>().HasMany(a => a.Tracks).WithOne(r => r.Album).OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<Album>().HasMany(a => a.Notificaciones).WithOne(r => r.Album).OnDelete(DeleteBehavior.Cascade);

            //artista

            modelBuilder.Entity<Artista>().HasMany(a => a.GenerosMusicales).WithOne(r => r.Artista).OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<Artista>().HasMany(a => a.Suscriptores).WithOne(r => r.Artista).OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<Artista>().HasMany(a => a.Albumes).WithOne(r => r.Artista).OnDelete(DeleteBehavior.Cascade);


            //genero

            modelBuilder.Entity<Genero>().HasMany(a => a.Artistas).WithOne(r => r.Genero).OnDelete(DeleteBehavior.Cascade);

            //notas

            modelBuilder.Entity<Nota>().HasMany(a => a.Usuarios_DisLiked).WithOne(r => r.Nota).OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<Nota>().HasMany(a => a.Usuarios_Liked).WithOne(r => r.Nota).OnDelete(DeleteBehavior.Cascade); modelBuilder.Entity<Nota>().HasMany(a => a.Usuarios_DisLiked).WithOne(r => r.Nota).OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<Nota>().HasMany(a => a.Reportes).WithOne(r => r.Nota).OnDelete(DeleteBehavior.Cascade);

            //review

            modelBuilder.Entity<Review>().HasMany(a => a.Reportes).WithOne(r => r.Review).OnDelete(DeleteBehavior.Cascade);

            //roles

            modelBuilder.Entity<Rol>().HasMany(a => a.Usuarios).WithOne(r => r.Rol).OnDelete(DeleteBehavior.SetNull);

            //track
            modelBuilder.Entity<Track>().HasMany(a => a.Usuarios_Liked).WithOne(r => r.Track).OnDelete(DeleteBehavior.Cascade);

            //notificaciones
            modelBuilder.Entity<Notificacion>().HasOne(a => a.UserTo).WithMany(r => r.Notificaciones_Recibidas).OnDelete(DeleteBehavior.NoAction);

            //reportes
            modelBuilder.Entity<Reporte>().HasOne(a => a.Review).WithMany(r => r.Reportes).OnDelete(DeleteBehavior.NoAction);





        }

    }
}
