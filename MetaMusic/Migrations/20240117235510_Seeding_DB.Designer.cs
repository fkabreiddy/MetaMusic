﻿// <auto-generated />
using System;
using MetaMusic.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace MetaMusic.Migrations
{
    [DbContext(typeof(MetaMusicDbContext))]
    [Migration("20240117235510_Seeding_DB")]
    partial class Seeding_DB
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.14")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("MetaMusic.Data.Entities.Album", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<double>("Calificacion_Creador")
                        .HasColumnType("float");

                    b.Property<int?>("CreadorId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("FechaModificado")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("Fecha_Agregado")
                        .HasColumnType("datetime2");

                    b.Property<string>("Fecha_Publicacion")
                        .IsRequired()
                        .HasMaxLength(25)
                        .HasColumnType("nvarchar(25)");

                    b.Property<DateTime?>("Fecha_Publicacion_Formateada")
                        .HasColumnType("datetime2");

                    b.Property<string>("IdSpotify")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<bool>("Modificado")
                        .HasColumnType("bit");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Portada")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("Publicado")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.HasIndex("CreadorId");

                    b.ToTable("Albumes");
                });

            modelBuilder.Entity("MetaMusic.Data.Entities.Album_Artista", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int?>("AlbumId")
                        .HasColumnType("int");

                    b.Property<int?>("ArtistaId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("AlbumId");

                    b.HasIndex("ArtistaId");

                    b.ToTable("Album_Artista");
                });

            modelBuilder.Entity("MetaMusic.Data.Entities.Artista", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int?>("CreadorId")
                        .HasColumnType("int");

                    b.Property<string>("Foto_Perfil")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("SpotifyId")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.HasIndex("CreadorId");

                    b.ToTable("Artistas");
                });

            modelBuilder.Entity("MetaMusic.Data.Entities.Artista_Suscriptor", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("ArtistaId")
                        .HasColumnType("int");

                    b.Property<int>("UsuarioId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ArtistaId");

                    b.HasIndex("UsuarioId");

                    b.ToTable("Artista_Suscriptores");
                });

            modelBuilder.Entity("MetaMusic.Data.Entities.Busqueda", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Contenido")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<int?>("UsuarioId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UsuarioId");

                    b.ToTable("Busquedas");
                });

            modelBuilder.Entity("MetaMusic.Data.Entities.Calificacion", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int?>("AlbumId")
                        .HasColumnType("int");

                    b.Property<double>("Numero")
                        .HasColumnType("float");

                    b.Property<int?>("UsuarioId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("AlbumId");

                    b.HasIndex("UsuarioId");

                    b.ToTable("Calificaciones");
                });

            modelBuilder.Entity("MetaMusic.Data.Entities.Genero", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.HasKey("Id");

                    b.ToTable("Generos");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Nombre = "R&B"
                        },
                        new
                        {
                            Id = 2,
                            Nombre = "Pop"
                        },
                        new
                        {
                            Id = 3,
                            Nombre = "Country"
                        },
                        new
                        {
                            Id = 4,
                            Nombre = "Reggae"
                        },
                        new
                        {
                            Id = 5,
                            Nombre = "Dance"
                        },
                        new
                        {
                            Id = 6,
                            Nombre = "Folk"
                        },
                        new
                        {
                            Id = 7,
                            Nombre = "Indie"
                        },
                        new
                        {
                            Id = 8,
                            Nombre = "Blues"
                        },
                        new
                        {
                            Id = 9,
                            Nombre = "Soul"
                        },
                        new
                        {
                            Id = 10,
                            Nombre = "Funk"
                        },
                        new
                        {
                            Id = 11,
                            Nombre = "Metal"
                        },
                        new
                        {
                            Id = 12,
                            Nombre = "Rock"
                        },
                        new
                        {
                            Id = 13,
                            Nombre = "Pop Urbano"
                        },
                        new
                        {
                            Id = 14,
                            Nombre = "Regueton"
                        },
                        new
                        {
                            Id = 15,
                            Nombre = "Bachata"
                        },
                        new
                        {
                            Id = 16,
                            Nombre = "Merengue"
                        },
                        new
                        {
                            Id = 17,
                            Nombre = "Electro-Pop"
                        },
                        new
                        {
                            Id = 18,
                            Nombre = "Electronica"
                        },
                        new
                        {
                            Id = 19,
                            Nombre = "Punk"
                        },
                        new
                        {
                            Id = 20,
                            Nombre = "Rap"
                        },
                        new
                        {
                            Id = 21,
                            Nombre = "Experimental"
                        },
                        new
                        {
                            Id = 22,
                            Nombre = "Hyper-Pop"
                        },
                        new
                        {
                            Id = 23,
                            Nombre = "Alternativa"
                        },
                        new
                        {
                            Id = 24,
                            Nombre = "Classical"
                        },
                        new
                        {
                            Id = 25,
                            Nombre = "Disco"
                        },
                        new
                        {
                            Id = 26,
                            Nombre = "Hip-Hop"
                        });
                });

            modelBuilder.Entity("MetaMusic.Data.Entities.Genero_Artista", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int?>("ArtistaId")
                        .HasColumnType("int");

                    b.Property<int?>("GeneroId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ArtistaId");

                    b.HasIndex("GeneroId");

                    b.ToTable("Genero_Artistas");
                });

            modelBuilder.Entity("MetaMusic.Data.Entities.Nota", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int?>("AlbumId")
                        .HasColumnType("int");

                    b.Property<int>("Cantidad_Dislikes")
                        .HasColumnType("int");

                    b.Property<int>("Cantidad_Likes")
                        .HasColumnType("int");

                    b.Property<string>("Contenido")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<int?>("CreadorId")
                        .HasColumnType("int");

                    b.Property<DateTime>("Fecha_Creacion")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("AlbumId");

                    b.HasIndex("CreadorId");

                    b.ToTable("Notas");
                });

            modelBuilder.Entity("MetaMusic.Data.Entities.Notificacion", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int?>("AlbumId")
                        .HasColumnType("int");

                    b.Property<DateTime>("Fecha_Creacion")
                        .HasColumnType("datetime2");

                    b.Property<string>("Titulo")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<int?>("UserFromId")
                        .HasColumnType("int");

                    b.Property<int>("UserToId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("AlbumId");

                    b.HasIndex("UserFromId");

                    b.HasIndex("UserToId");

                    b.ToTable("Notificacions");
                });

            modelBuilder.Entity("MetaMusic.Data.Entities.Peticion", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("Acumulaciones")
                        .HasColumnType("int");

                    b.Property<string>("AlbumNombre")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("AlbumPortada")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("AlbumSpotifyId")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("ArtistaFoto")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("ArtistaNombre")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("ArtistaSpotifyId")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<DateTime>("UltimaPeticionFecha")
                        .HasColumnType("datetime2");

                    b.Property<int>("UsuarioId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UsuarioId");

                    b.ToTable("Peticiones");
                });

            modelBuilder.Entity("MetaMusic.Data.Entities.Reporte", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Contenido")
                        .IsRequired()
                        .HasMaxLength(120)
                        .HasColumnType("nvarchar(120)");

                    b.Property<DateTime>("Fecha_Creacion")
                        .HasColumnType("datetime2");

                    b.Property<int?>("NotaId")
                        .HasColumnType("int");

                    b.Property<int?>("ReviewId")
                        .HasColumnType("int");

                    b.Property<string>("Titulo")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<int?>("UsuarioId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("NotaId");

                    b.HasIndex("ReviewId");

                    b.HasIndex("UsuarioId");

                    b.ToTable("Reportes");
                });

            modelBuilder.Entity("MetaMusic.Data.Entities.Review", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("AlbumId")
                        .HasColumnType("int");

                    b.Property<string>("Contenido")
                        .IsRequired()
                        .HasMaxLength(3000)
                        .HasColumnType("nvarchar(3000)");

                    b.Property<int?>("CreadorId")
                        .HasColumnType("int");

                    b.Property<string>("Titulo")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Id");

                    b.HasIndex("AlbumId")
                        .IsUnique();

                    b.HasIndex("CreadorId");

                    b.ToTable("Reviews");
                });

            modelBuilder.Entity("MetaMusic.Data.Entities.Rol", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Tipo")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Roles");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Tipo = "Normal"
                        },
                        new
                        {
                            Id = 2,
                            Tipo = "Staff"
                        });
                });

            modelBuilder.Entity("MetaMusic.Data.Entities.Track", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("AlbumId")
                        .HasColumnType("int");

                    b.Property<int>("Cantidad_Likes")
                        .HasColumnType("int");

                    b.Property<string>("Titulo")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("AlbumId");

                    b.ToTable("Tracks");
                });

            modelBuilder.Entity("MetaMusic.Data.Entities.Usuario", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Biografia")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Correo")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CorreoNormalizado")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FotoDePerfil")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("RolId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("RolId");

                    b.ToTable("Usuarios");
                });

            modelBuilder.Entity("MetaMusic.Data.Entities.Usuario_Dislike_Nota", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int?>("NotaId")
                        .HasColumnType("int");

                    b.Property<int?>("UsuarioId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("NotaId");

                    b.HasIndex("UsuarioId");

                    b.ToTable("Usuario_Dislike_Notas");
                });

            modelBuilder.Entity("MetaMusic.Data.Entities.Usuario_Like_Nota", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int?>("NotaId")
                        .HasColumnType("int");

                    b.Property<int?>("UsuarioId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("NotaId");

                    b.HasIndex("UsuarioId");

                    b.ToTable("Usuario_Like_Notas");
                });

            modelBuilder.Entity("MetaMusic.Data.Entities.Usuario_Like_Track", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int?>("TrackId")
                        .HasColumnType("int");

                    b.Property<int?>("UsuarioId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("TrackId");

                    b.HasIndex("UsuarioId");

                    b.ToTable("Usuario_Like_Tracks");
                });

            modelBuilder.Entity("MetaMusic.Data.Entities.Album", b =>
                {
                    b.HasOne("MetaMusic.Data.Entities.Usuario", "Creador")
                        .WithMany("Albumes_Publicados")
                        .HasForeignKey("CreadorId")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.Navigation("Creador");
                });

            modelBuilder.Entity("MetaMusic.Data.Entities.Album_Artista", b =>
                {
                    b.HasOne("MetaMusic.Data.Entities.Album", "Album")
                        .WithMany("Artistas")
                        .HasForeignKey("AlbumId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("MetaMusic.Data.Entities.Artista", "Artista")
                        .WithMany("Albumes")
                        .HasForeignKey("ArtistaId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.Navigation("Album");

                    b.Navigation("Artista");
                });

            modelBuilder.Entity("MetaMusic.Data.Entities.Artista", b =>
                {
                    b.HasOne("MetaMusic.Data.Entities.Usuario", "Creador")
                        .WithMany("Artistas_Creados")
                        .HasForeignKey("CreadorId")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.Navigation("Creador");
                });

            modelBuilder.Entity("MetaMusic.Data.Entities.Artista_Suscriptor", b =>
                {
                    b.HasOne("MetaMusic.Data.Entities.Artista", "Artista")
                        .WithMany("Suscriptores")
                        .HasForeignKey("ArtistaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MetaMusic.Data.Entities.Usuario", "Usuario")
                        .WithMany("Artistas_Suscritos")
                        .HasForeignKey("UsuarioId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Artista");

                    b.Navigation("Usuario");
                });

            modelBuilder.Entity("MetaMusic.Data.Entities.Busqueda", b =>
                {
                    b.HasOne("MetaMusic.Data.Entities.Usuario", "Usuario")
                        .WithMany("Busquedas")
                        .HasForeignKey("UsuarioId")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.Navigation("Usuario");
                });

            modelBuilder.Entity("MetaMusic.Data.Entities.Calificacion", b =>
                {
                    b.HasOne("MetaMusic.Data.Entities.Album", "Album")
                        .WithMany("Calificaciones")
                        .HasForeignKey("AlbumId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("MetaMusic.Data.Entities.Usuario", "Usuario")
                        .WithMany("Calificaciones")
                        .HasForeignKey("UsuarioId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.Navigation("Album");

                    b.Navigation("Usuario");
                });

            modelBuilder.Entity("MetaMusic.Data.Entities.Genero_Artista", b =>
                {
                    b.HasOne("MetaMusic.Data.Entities.Artista", "Artista")
                        .WithMany("GenerosMusicales")
                        .HasForeignKey("ArtistaId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("MetaMusic.Data.Entities.Genero", "Genero")
                        .WithMany("Artistas")
                        .HasForeignKey("GeneroId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.Navigation("Artista");

                    b.Navigation("Genero");
                });

            modelBuilder.Entity("MetaMusic.Data.Entities.Nota", b =>
                {
                    b.HasOne("MetaMusic.Data.Entities.Album", "Album")
                        .WithMany("Notas")
                        .HasForeignKey("AlbumId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("MetaMusic.Data.Entities.Usuario", "Creador")
                        .WithMany("Notas")
                        .HasForeignKey("CreadorId");

                    b.Navigation("Album");

                    b.Navigation("Creador");
                });

            modelBuilder.Entity("MetaMusic.Data.Entities.Notificacion", b =>
                {
                    b.HasOne("MetaMusic.Data.Entities.Album", "Album")
                        .WithMany("Notificaciones")
                        .HasForeignKey("AlbumId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("MetaMusic.Data.Entities.Usuario", "UserFrom")
                        .WithMany("Notificaciones_Hechas")
                        .HasForeignKey("UserFromId")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.HasOne("MetaMusic.Data.Entities.Usuario", "UserTo")
                        .WithMany("Notificaciones_Recibidas")
                        .HasForeignKey("UserToId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Album");

                    b.Navigation("UserFrom");

                    b.Navigation("UserTo");
                });

            modelBuilder.Entity("MetaMusic.Data.Entities.Peticion", b =>
                {
                    b.HasOne("MetaMusic.Data.Entities.Usuario", "Usuario")
                        .WithMany("Peticiones")
                        .HasForeignKey("UsuarioId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Usuario");
                });

            modelBuilder.Entity("MetaMusic.Data.Entities.Reporte", b =>
                {
                    b.HasOne("MetaMusic.Data.Entities.Nota", "Nota")
                        .WithMany("Reportes")
                        .HasForeignKey("NotaId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("MetaMusic.Data.Entities.Review", "Review")
                        .WithMany("Reportes")
                        .HasForeignKey("ReviewId")
                        .OnDelete(DeleteBehavior.NoAction);

                    b.HasOne("MetaMusic.Data.Entities.Usuario", "Usuario")
                        .WithMany("Reportes")
                        .HasForeignKey("UsuarioId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.Navigation("Nota");

                    b.Navigation("Review");

                    b.Navigation("Usuario");
                });

            modelBuilder.Entity("MetaMusic.Data.Entities.Review", b =>
                {
                    b.HasOne("MetaMusic.Data.Entities.Album", "Album")
                        .WithOne("Review")
                        .HasForeignKey("MetaMusic.Data.Entities.Review", "AlbumId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MetaMusic.Data.Entities.Usuario", "Creador")
                        .WithMany("Reviews")
                        .HasForeignKey("CreadorId");

                    b.Navigation("Album");

                    b.Navigation("Creador");
                });

            modelBuilder.Entity("MetaMusic.Data.Entities.Track", b =>
                {
                    b.HasOne("MetaMusic.Data.Entities.Album", "Album")
                        .WithMany("Tracks")
                        .HasForeignKey("AlbumId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Album");
                });

            modelBuilder.Entity("MetaMusic.Data.Entities.Usuario", b =>
                {
                    b.HasOne("MetaMusic.Data.Entities.Rol", "Rol")
                        .WithMany("Usuarios")
                        .HasForeignKey("RolId")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.Navigation("Rol");
                });

            modelBuilder.Entity("MetaMusic.Data.Entities.Usuario_Dislike_Nota", b =>
                {
                    b.HasOne("MetaMusic.Data.Entities.Nota", "Nota")
                        .WithMany("Usuarios_DisLiked")
                        .HasForeignKey("NotaId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("MetaMusic.Data.Entities.Usuario", "Usuario")
                        .WithMany("Notas_DisLikeadas")
                        .HasForeignKey("UsuarioId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.Navigation("Nota");

                    b.Navigation("Usuario");
                });

            modelBuilder.Entity("MetaMusic.Data.Entities.Usuario_Like_Nota", b =>
                {
                    b.HasOne("MetaMusic.Data.Entities.Nota", "Nota")
                        .WithMany("Usuarios_Liked")
                        .HasForeignKey("NotaId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("MetaMusic.Data.Entities.Usuario", "Usuario")
                        .WithMany("Notas_Likeadas")
                        .HasForeignKey("UsuarioId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.Navigation("Nota");

                    b.Navigation("Usuario");
                });

            modelBuilder.Entity("MetaMusic.Data.Entities.Usuario_Like_Track", b =>
                {
                    b.HasOne("MetaMusic.Data.Entities.Track", "Track")
                        .WithMany("Usuarios_Liked")
                        .HasForeignKey("TrackId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("MetaMusic.Data.Entities.Usuario", "Usuario")
                        .WithMany("Liked_Tracks")
                        .HasForeignKey("UsuarioId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.Navigation("Track");

                    b.Navigation("Usuario");
                });

            modelBuilder.Entity("MetaMusic.Data.Entities.Album", b =>
                {
                    b.Navigation("Artistas");

                    b.Navigation("Calificaciones");

                    b.Navigation("Notas");

                    b.Navigation("Notificaciones");

                    b.Navigation("Review");

                    b.Navigation("Tracks");
                });

            modelBuilder.Entity("MetaMusic.Data.Entities.Artista", b =>
                {
                    b.Navigation("Albumes");

                    b.Navigation("GenerosMusicales");

                    b.Navigation("Suscriptores");
                });

            modelBuilder.Entity("MetaMusic.Data.Entities.Genero", b =>
                {
                    b.Navigation("Artistas");
                });

            modelBuilder.Entity("MetaMusic.Data.Entities.Nota", b =>
                {
                    b.Navigation("Reportes");

                    b.Navigation("Usuarios_DisLiked");

                    b.Navigation("Usuarios_Liked");
                });

            modelBuilder.Entity("MetaMusic.Data.Entities.Review", b =>
                {
                    b.Navigation("Reportes");
                });

            modelBuilder.Entity("MetaMusic.Data.Entities.Rol", b =>
                {
                    b.Navigation("Usuarios");
                });

            modelBuilder.Entity("MetaMusic.Data.Entities.Track", b =>
                {
                    b.Navigation("Usuarios_Liked");
                });

            modelBuilder.Entity("MetaMusic.Data.Entities.Usuario", b =>
                {
                    b.Navigation("Albumes_Publicados");

                    b.Navigation("Artistas_Creados");

                    b.Navigation("Artistas_Suscritos");

                    b.Navigation("Busquedas");

                    b.Navigation("Calificaciones");

                    b.Navigation("Liked_Tracks");

                    b.Navigation("Notas");

                    b.Navigation("Notas_DisLikeadas");

                    b.Navigation("Notas_Likeadas");

                    b.Navigation("Notificaciones_Hechas");

                    b.Navigation("Notificaciones_Recibidas");

                    b.Navigation("Peticiones");

                    b.Navigation("Reportes");

                    b.Navigation("Reviews");
                });
#pragma warning restore 612, 618
        }
    }
}
