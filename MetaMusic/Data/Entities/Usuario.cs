using MetaMusic.Data.Extensiones;
using MetaMusic.Data.OtherEntities;
using MetaMusic.Data.Request;
using MetaMusic.Data.Responses;
using System.ComponentModel.DataAnnotations;

namespace MetaMusic.Data.Entities
{
    public class Usuario
    {

        [Key]
        public int Id { get; set; }



        [StringLength(100)]
        public string Nombre { get; set; } = null!;
        [StringLength(150)]
        public string Correo { get; set; } = "";

        [StringLength(150)]
        public string CorreoNormalizado { get; set; } = "";
        public string FotoDePerfil { get; set; } = null!;

        [StringLength(100)]
        public string Biografia { get; set; } = null!;
        public Rol? Rol { get; set; } = new Rol();

        public List<Artista> Artistas_Creados { get; set; } = new List<Artista>();

        public List<Album> Albumes_Publicados { get; set; } = new List<Album>();

        public List<Usuario_Dislike_Nota> Notas_DisLikeadas { get; set; } = new List<Usuario_Dislike_Nota>();
        public List<Usuario_Like_Nota> Notas_Likeadas { get; set; } = new List<Usuario_Like_Nota>();
        public List<Usuario_Like_Track> Liked_Tracks { get; set; } = new List<Usuario_Like_Track>(); 
        public List<Review> Reviews { get; set; } = new List<Review>();
        public List<Nota> Notas { get; set; } = new List<Nota>();
        public List<Notificacion> Notificaciones_Hechas { get; set; } = new List<Notificacion>();
        public List<Notificacion> Notificaciones_Recibidas { get; set; } = new List<Notificacion>();
        public List<Busqueda> Busquedas { get; set; } = new List<Busqueda>();

        public List<Reporte> Reportes { get; set; } = new List<Reporte>();
        public List<Artista_Suscriptor> Artistas_Suscritos { get; set; } = new List<Artista_Suscriptor>();
        public List<Calificacion> Calificaciones { get; set; } = new List<Calificacion>();
        

        public List<Peticion> Peticiones { get; set; } = new List<Peticion>();
        public LoginResponse ToLoginResponse() => new LoginResponse()
        {
            Id = this.Id,
            Nombre = this.Nombre,
            Correo = this.Correo,
      
            Rol = this.Rol ?? new(),
           
            // Agrega otras propiedades si es necesario
        };
        public UsuarioResponse ToResponse() => new UsuarioResponse()
        {
            Id = this.Id,
            Nombre = this.Nombre,
            Correo = this.Correo,
            FotoDePerfil = this.FotoDePerfil,
            Biografia = this.Biografia,
            Rol = this.Rol ?? new(),
            Artistas_Creados = this.Artistas_Creados,
            Albumes_Publicados = this.Albumes_Publicados,
            Notas_DisLikeadas = this.Notas_DisLikeadas,
            Notas_Likeadas = this.Notas_Likeadas,
            Liked_Tracks = this.Liked_Tracks,
            Reviews = this.Reviews,
            Notas = this.Notas,
            Notificaciones_Hechas = this.Notificaciones_Hechas,
            Notificaciones_Recibidas = this.Notificaciones_Recibidas,
            Busquedas = this.Busquedas,
         
            Reportes = this.Reportes,
            Artistas_Suscritos = this.Artistas_Suscritos,
            Calificaciones = this.Calificaciones,
            Peticiones = this.Peticiones,
            CorreoNormalizado = this.CorreoNormalizado,
           

            // Agrega otras propiedades si es necesario
        };
        public static Usuario Crear(UsuarioRequest request) => new Usuario()
        {
            Id = request.Id,
            Nombre = request.Nombre,
            Correo = request.Correo,
            FotoDePerfil = request.FotoDePerfil,
            Biografia = request.Biografia,
            Rol = request.Rol,
            Artistas_Creados = request.Artistas_Creados,
            Albumes_Publicados = request.Albumes_Publicados,
            Notas_DisLikeadas = request.Notas_DisLikeadas,
            Notas_Likeadas = request.Notas_Likeadas,
            Liked_Tracks = request.Liked_Tracks,
            Reviews = request.Reviews,
            Notas = request.Notas,
            Notificaciones_Hechas = request.Notificaciones_Hechas,
            Notificaciones_Recibidas = request.Notificaciones_Recibidas,
            Busquedas = request.Busquedas,
          
            Reportes = request.Reportes,
            Artistas_Suscritos = request.Artistas_Suscritos,
            Calificaciones = request.Calificaciones,
            Peticiones = request.Peticiones,
            CorreoNormalizado = request.Correo.Normalizar(),
            

            // Agrega otras propiedades si es necesario
        };

        public bool Modificar(UsuarioRequest usuarioResponse)
        {
            bool modificacion = false;

            if (this.Nombre != usuarioResponse.Nombre)
            {
                this.Nombre = usuarioResponse.Nombre;
                modificacion = true;
            }
          
            if (this.Correo != usuarioResponse.Correo)
            {
                this.Correo = usuarioResponse.Correo;
                modificacion = true;
            }
            if (this.CorreoNormalizado != usuarioResponse.CorreoNormalizado)
            {
                this.CorreoNormalizado = usuarioResponse.CorreoNormalizado;
                modificacion = true;
            }
            if (this.FotoDePerfil != usuarioResponse.FotoDePerfil)
            {
                this.FotoDePerfil = usuarioResponse.FotoDePerfil;
                modificacion = true;
            }

            if (this.Biografia != usuarioResponse.Biografia)
            {
                this.Biografia = usuarioResponse.Biografia;
                modificacion = true;
            }

            if (!Rol!.Equals(usuarioResponse.Rol))
            {
                Rol = usuarioResponse.Rol;
                modificacion = true;
            }

            if (!Artistas_Creados.SequenceEqual(usuarioResponse.Artistas_Creados))
            {
                Artistas_Creados = usuarioResponse.Artistas_Creados;
                modificacion = true;
            }
            if (this.Peticiones != usuarioResponse.Peticiones)
            {
                Peticiones = usuarioResponse.Peticiones;
                modificacion = true;
            }
            if (!Albumes_Publicados.SequenceEqual(usuarioResponse.Albumes_Publicados))
            {
                Albumes_Publicados = usuarioResponse.Albumes_Publicados;
                modificacion = true;
            }

            if (!Notas_DisLikeadas.SequenceEqual(usuarioResponse.Notas_DisLikeadas))
            {
                Notas_DisLikeadas = usuarioResponse.Notas_DisLikeadas;
                modificacion = true;
            }

            if (!Notas_Likeadas.SequenceEqual(usuarioResponse.Notas_Likeadas))
            {
                Notas_Likeadas = usuarioResponse.Notas_Likeadas;
                modificacion = true;
            }

            if (!Liked_Tracks.SequenceEqual(usuarioResponse.Liked_Tracks))
            {
                Liked_Tracks = usuarioResponse.Liked_Tracks;
                modificacion = true;
            }

            if (!Reviews.SequenceEqual(usuarioResponse.Reviews))
            {
                Reviews = usuarioResponse.Reviews;
                modificacion = true;
            }

            if (!Notas.SequenceEqual(usuarioResponse.Notas))
            {
                Notas = usuarioResponse.Notas;
                modificacion = true;
            }

            if (!Notificaciones_Hechas.SequenceEqual(usuarioResponse.Notificaciones_Hechas))
            {
                Notificaciones_Hechas = usuarioResponse.Notificaciones_Hechas;
                modificacion = true;
            }

            if (!Notificaciones_Recibidas.SequenceEqual(usuarioResponse.Notificaciones_Recibidas))
            {
                Notificaciones_Recibidas = usuarioResponse.Notificaciones_Recibidas;
                modificacion = true;
            }


            if (!Busquedas.SequenceEqual(usuarioResponse.Busquedas))
            {
                Busquedas = usuarioResponse.Busquedas;
                modificacion = true;
            }



            if (!Reportes.SequenceEqual(usuarioResponse.Reportes))
            {
                Reportes = usuarioResponse.Reportes;
                modificacion = true;
            }

            if (!Artistas_Suscritos.SequenceEqual(usuarioResponse.Artistas_Suscritos))
            {
                Artistas_Suscritos = usuarioResponse.Artistas_Suscritos;
                modificacion = true;
            }

            if (!Calificaciones.SequenceEqual(usuarioResponse.Calificaciones))
            {
                Calificaciones = usuarioResponse.Calificaciones;
                modificacion = true;
            }

            // Agrega otras comparaciones y actualizaciones si es necesario

            return modificacion;
        }
    }
}
