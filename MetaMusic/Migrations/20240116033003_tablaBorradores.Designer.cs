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
    [Migration("20240116033003_tablaBorradores")]
    partial class tablaBorradores
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

                    b.Property<int?>("CreadorId")
                        .HasColumnType("int");

                    b.Property<DateTime>("Fecha_Agregado")
                        .HasColumnType("datetime2");

                    b.Property<string>("Fecha_Publicacion")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("Fecha_Publicacion_Formateada")
                        .HasColumnType("datetime2");

                    b.Property<string>("IdSpotify")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("Modificado")
                        .HasColumnType("bit");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Portada")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("Publicado")
                        .HasColumnType("bit");

                    b.Property<bool>("YaExiste")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.HasIndex("CreadorId");

                    b.ToTable("Album");
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
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SpotifyId")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

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
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UsuarioId")
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

                    b.Property<int>("Numero")
                        .HasColumnType("int");

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
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Generos");
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

                    b.Property<int>("AlbumId")
                        .HasColumnType("int");

                    b.Property<string>("Contenido")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("CreadorId")
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

                    b.Property<string>("Titulo")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

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

                    b.Property<int?>("AlbumId")
                        .HasColumnType("int");

                    b.Property<int>("ArtistaId")
                        .HasColumnType("int");

                    b.Property<DateTime>("UltimaPeticionFecha")
                        .HasColumnType("datetime2");

                    b.Property<int>("UsuarioId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("AlbumId");

                    b.HasIndex("ArtistaId");

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
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("NotaId")
                        .HasColumnType("int");

                    b.Property<int?>("ReviewId")
                        .HasColumnType("int");

                    b.Property<string>("Titulo")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UsuarioId")
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

                    b.Property<string>("Contenido")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("CreadorId")
                        .HasColumnType("int");

                    b.Property<int>("IdAlbum")
                        .HasColumnType("int");

                    b.Property<string>("Titulo")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("CreadorId");

                    b.HasIndex("IdAlbum")
                        .IsUnique();

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
                });

            modelBuilder.Entity("MetaMusic.Data.Entities.Suscripcion", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int?>("SuscriptorId")
                        .HasColumnType("int");

                    b.Property<int?>("UsuarioId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("SuscriptorId");

                    b.HasIndex("UsuarioId");

                    b.ToTable("Suscripciones");
                });

            modelBuilder.Entity("MetaMusic.Data.Entities.Track", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("AlbumId")
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

                    b.Property<int>("RolId")
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
                        .OnDelete(DeleteBehavior.NoAction);

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
                        .OnDelete(DeleteBehavior.NoAction);

                    b.Navigation("Album");

                    b.Navigation("Artista");
                });

            modelBuilder.Entity("MetaMusic.Data.Entities.Artista", b =>
                {
                    b.HasOne("MetaMusic.Data.Entities.Usuario", "Creador")
                        .WithMany("Artistas_Creados")
                        .HasForeignKey("CreadorId")
                        .OnDelete(DeleteBehavior.NoAction);

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
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Usuario");
                });

            modelBuilder.Entity("MetaMusic.Data.Entities.Calificacion", b =>
                {
                    b.HasOne("MetaMusic.Data.Entities.Album", "Album")
                        .WithMany("Calificaciones")
                        .HasForeignKey("AlbumId")
                        .OnDelete(DeleteBehavior.NoAction);

                    b.HasOne("MetaMusic.Data.Entities.Usuario", "Usuario")
                        .WithMany("Calificaciones")
                        .HasForeignKey("UsuarioId")
                        .OnDelete(DeleteBehavior.NoAction);

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
                        .HasForeignKey("GeneroId");

                    b.Navigation("Artista");

                    b.Navigation("Genero");
                });

            modelBuilder.Entity("MetaMusic.Data.Entities.Nota", b =>
                {
                    b.HasOne("MetaMusic.Data.Entities.Album", "Album")
                        .WithMany("Notas")
                        .HasForeignKey("AlbumId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("MetaMusic.Data.Entities.Usuario", "Creador")
                        .WithMany("Notas")
                        .HasForeignKey("CreadorId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Album");

                    b.Navigation("Creador");
                });

            modelBuilder.Entity("MetaMusic.Data.Entities.Notificacion", b =>
                {
                    b.HasOne("MetaMusic.Data.Entities.Album", "Album")
                        .WithMany("Notificaciones")
                        .HasForeignKey("AlbumId")
                        .OnDelete(DeleteBehavior.NoAction);

                    b.HasOne("MetaMusic.Data.Entities.Usuario", "UserFrom")
                        .WithMany("Notificaciones_Hechas")
                        .HasForeignKey("UserFromId")
                        .OnDelete(DeleteBehavior.NoAction);

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
                    b.HasOne("MetaMusic.Data.Entities.Album", "Album")
                        .WithMany("Peticiones")
                        .HasForeignKey("AlbumId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("MetaMusic.Data.Entities.Artista", "Artista")
                        .WithMany("Peticiones")
                        .HasForeignKey("ArtistaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MetaMusic.Data.Entities.Usuario", "Usuario")
                        .WithMany("Peticiones")
                        .HasForeignKey("UsuarioId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Album");

                    b.Navigation("Artista");

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
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Nota");

                    b.Navigation("Review");

                    b.Navigation("Usuario");
                });

            modelBuilder.Entity("MetaMusic.Data.Entities.Review", b =>
                {
                    b.HasOne("MetaMusic.Data.Entities.Usuario", "Creador")
                        .WithMany("Reviews")
                        .HasForeignKey("CreadorId")
                        .OnDelete(DeleteBehavior.NoAction);

                    b.HasOne("MetaMusic.Data.Entities.Album", "Album")
                        .WithOne("Review")
                        .HasForeignKey("MetaMusic.Data.Entities.Review", "IdAlbum")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Album");

                    b.Navigation("Creador");
                });

            modelBuilder.Entity("MetaMusic.Data.Entities.Suscripcion", b =>
                {
                    b.HasOne("MetaMusic.Data.Entities.Usuario", "Suscriptor")
                        .WithMany("Suscripciones")
                        .HasForeignKey("SuscriptorId")
                        .OnDelete(DeleteBehavior.NoAction);

                    b.HasOne("MetaMusic.Data.Entities.Usuario", "Usuario")
                        .WithMany("Suscriptores")
                        .HasForeignKey("UsuarioId")
                        .OnDelete(DeleteBehavior.NoAction);

                    b.Navigation("Suscriptor");

                    b.Navigation("Usuario");
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
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

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
                        .OnDelete(DeleteBehavior.NoAction);

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
                        .OnDelete(DeleteBehavior.NoAction);

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

                    b.Navigation("Peticiones");

                    b.Navigation("Review");

                    b.Navigation("Tracks");
                });

            modelBuilder.Entity("MetaMusic.Data.Entities.Artista", b =>
                {
                    b.Navigation("Albumes");

                    b.Navigation("GenerosMusicales");

                    b.Navigation("Peticiones");

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

                    b.Navigation("Suscripciones");

                    b.Navigation("Suscriptores");
                });
#pragma warning restore 612, 618
        }
    }
}
