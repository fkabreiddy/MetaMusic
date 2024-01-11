﻿using MetaMusic.Data.Context;
using MetaMusic.Data.Entities;
using MetaMusic.Data.OtherEntities;
using MetaMusic.Data.Request;
using MetaMusic.Data.Responses;
using MetaMusic.Pages.MainComponents;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace MetaMusic.Data.Services
{
    public class ArtistaService : IArtistaService
    {
        private readonly IMetaMusicDbContext dbContext;
        private readonly IGoogleAuthService googleAuthService;

        public ArtistaService(IGoogleAuthService googleAuthService, IMetaMusicDbContext dbContext)
        {
            this.googleAuthService = googleAuthService;
            this.dbContext =
                dbContext;
        }

        public async Task<Result<ArtistaResponse>> CrearArtista(ArtistaRequest request)
        {
            try
            {
                if (request is null)
                    return new Result<ArtistaResponse>()
                    {
                        Message = "Bad Request",
                        Success = false
                    };

                List<Genero_Artista> generos = new List<Genero_Artista>();

                generos = request.GenerosMusicales.ToList();

                var usuarioactual = await googleAuthService.GetCurrentUser();

                if (usuarioactual.Data is null)
                    return new Result<ArtistaResponse>()
                    {
                        Message = "No estas registado",
                        Success = false
                    };


                var creador = await dbContext.Usuarios.Include(U => U.Rol).FirstOrDefaultAsync(u => u.Id == usuarioactual.Data.Id);

                if (creador is null || creador.Rol.Tipo != "Staff")
                    return new Result<ArtistaResponse>()
                    {
                        Message = "No estas autorizado para agregar contenido",
                        Success = false
                    };

                request.Creador = creador;
                request.GenerosMusicales.Clear();
                var newArtist = Artista.Crear(request);
                await dbContext.Artistas.AddAsync(newArtist);
                await dbContext.SaveChangesAsync();

                var artist = await dbContext.Artistas.FirstOrDefaultAsync(a => a.Id == newArtist.Id);

                if (generos.Count() >= 0)
                {
                    foreach (var genero in generos)
                    {
                        var genre = await dbContext.Generos.FirstOrDefaultAsync(g => g.Id == genero.Genero.Id);

                        if (genre is not null)
                            dbContext.Genero_Artistas.Add(new Genero_Artista() { Artista = artist, Genero = genre });

                    };

                    await dbContext.SaveChangesAsync();
                }

                return new Result<ArtistaResponse>()
                {
                    Data = newArtist.ToResponse(),
                    Message = "Creacion Exitosa",
                    Success = true
                };

            }
            catch (Exception e)
            {
                return new Result<ArtistaResponse>()
                {

                    Message = e.InnerException?.Message ?? e.Message,
                    Success = false
                };
            }
        }
        public async Task<Result<List<ArtistaResponse>>> ConsultarTodosLosArtistas()
        {
            try
            {

                var artistas = await dbContext.Artistas.Include(a => a.GenerosMusicales).ThenInclude(g => g.Genero).Include(a => a.Suscriptores).ToListAsync();


                if (artistas is null)
                {
                    return new Result<List<ArtistaResponse>>() { Message = "No hay Artistas", Success = false };

                }

                return new Result<List<ArtistaResponse>>() { Data = artistas.Select(a => a.ToResponse()).ToList(), Success = true };
            }
            catch (Exception e)
            {
                return new Result<List<ArtistaResponse>>() { Message = e.InnerException?.Message ?? e.Message, Success = false };
            }
        }
        public async Task<Result<ArtistaResponse>> ConsultarArtista(string spotifyId)
        {
            try
            {
                var artista = await dbContext.Artistas.Include(a => a.GenerosMusicales).ThenInclude(g => g.Genero).Include(a => a.Suscriptores).ThenInclude(s => s.Usuario).FirstOrDefaultAsync(a => a.SpotifyId == spotifyId);

                if (artista is null)
                    return new Result<ArtistaResponse>() { Message = "No existe el Artista", Success = false };


                return new Result<ArtistaResponse>() { Success = true, Data = artista.ToResponse() };

            }
            catch (Exception e)
            {
                return new Result<ArtistaResponse>() { Success = false, Message = e.InnerException?.Message ?? e.Message };
            }
        }

        public async Task<Result<ArtistaResponse>> Suscribirse(ArtistaResponse artista)
        {
            try
            {
                var usuarioactual = await googleAuthService.GetCurrentUser();

                if (usuarioactual.Data is null)
                    return new Result<ArtistaResponse>() { Success = false, Message = "No te puedes suscribir por que no estas logeado" };

                var usuario = await dbContext.Usuarios.FirstOrDefaultAsync(u => u.Id == usuarioactual.Data.Id);

                if (usuario is null)
                    return new Result<ArtistaResponse>()
                    {
                        Success = false,
                        Message = "No estas registrado"
                    };

                var ar = await dbContext.Artistas.FirstOrDefaultAsync(a => a.Id == artista.Id);

                if (ar is null)
                    return new Result<ArtistaResponse>()
                    {
                        Success = false,
                        Message = "El artista no existe"
                    };


                await dbContext.Artista_Suscriptores.AddAsync(new Artista_Suscriptor() { Artista = ar, Usuario = usuario });

                await dbContext.SaveChangesAsync();

                return new Result<ArtistaResponse>()
                {
                    Data = ar.ToResponse(),
                    Success = true,
                    Message = "Registro exitoso"
                };

            }
            catch (Exception e)
            {
                return new Result<ArtistaResponse>()
                {
                    Success = false,
                    Message = e.InnerException?.Message ?? e.Message
                };

            }
        }
        public async Task<Result<ArtistaResponse>> DesSuscribirse(ArtistaResponse artista)
        {
            try
            {
                var usuarioactual = await googleAuthService.GetCurrentUser();

                if (usuarioactual.Data is null)
                    return new Result<ArtistaResponse>() { Success = false, Message = "No te puedes suscribir por que no estas logeado" };

                var usuario = await dbContext.Usuarios.FirstOrDefaultAsync(u => u.Id == usuarioactual.Data.Id);

                if (usuario is null)
                    return new Result<ArtistaResponse>()
                    {
                        Success = false,
                        Message = "No estas registrado"
                    };

                var relacion = await dbContext.Artista_Suscriptores.FirstOrDefaultAsync(a => a.Artista.Id == artista.Id && a.Usuario.Id == usuario.Id);

                if (relacion is null)
                    return new Result<ArtistaResponse>()
                    {
                        Success = false,
                        Message = "El artista no existe"
                    };
                var ar = await dbContext.Artistas.FirstOrDefaultAsync(a => a.Id == artista.Id);

                dbContext.Artista_Suscriptores.Remove(relacion);

                await dbContext.SaveChangesAsync();


                if (ar is null)
                    return new Result<ArtistaResponse>()
                    {
                        Success = false,
                        Message = "El artista no existe"
                    };
                return new Result<ArtistaResponse>()
                {
                    Data = ar.ToResponse(),
                    Success = true,
                    Message = "Registro exitoso"
                };

            }
            catch (Exception e)
            {
                return new Result<ArtistaResponse>()
                {
                    Success = false,
                    Message = e.InnerException?.Message ?? e.Message
                };

            }
        }
        public async Task<Result<ArtistaResponse>> ModificarArtista(ArtistaRequest request)
        {
            try
            {
                var artista = await dbContext.Artistas.FirstOrDefaultAsync(a => a.SpotifyId == request.SpotifyId);

                if (artista is null)
                    return new Result<ArtistaResponse>() { Message = "No existe el Artista", Success = false };

                artista.Modificar(request);

                await dbContext.SaveChangesAsync();

                return new Result<ArtistaResponse>() { Success = true, Data = artista.ToResponse() };

            }
            catch (Exception e)
            {
                return new Result<ArtistaResponse>() { Success = false, Message = e.InnerException?.Message ?? e.Message };
            }
        }
        public async Task<Result<ArtistaResponse>> Eliminar(ArtistaRequest request)
        {
            try
            {
                var artista = await dbContext.Artistas.FirstOrDefaultAsync(a => a.SpotifyId == request.SpotifyId);

                if (artista is null)
                    return new Result<ArtistaResponse>() { Message = "No existe el Artista", Success = false };

                dbContext.Artistas.Remove(artista);

                await dbContext.SaveChangesAsync();

                return new Result<ArtistaResponse>() { Success = true, Message = "Se elimino el artista" };

            }
            catch (Exception e)
            {
                return new Result<ArtistaResponse>() { Success = false, Message = e.InnerException?.Message ?? e.Message };
            }
        }

    }
}
