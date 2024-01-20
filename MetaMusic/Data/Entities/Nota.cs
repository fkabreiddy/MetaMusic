using MetaMusic.Data.Request;
using MetaMusic.Data.Responses;
using System.ComponentModel.DataAnnotations;

namespace MetaMusic.Data.Entities
{
    public class Nota
    {
        [Key]
        public int Id { get; set; }


        [StringLength(400)]
        public string Contenido { get; set; } = null!;

        public Usuario? Creador { get; set; } = new Usuario();

        public int Cantidad_Dislikes { get; set; } = 0;

        public int Cantidad_Likes { get; set; } = 0;
        public DateTime Fecha_Creacion { get; set; }
        public Album? Album { get; set; } = new Album();
        

        public List<Usuario_Like_Nota> Usuarios_Liked = new List<Usuario_Like_Nota>();
     

        public List<Usuario_Dislike_Nota> Usuarios_DisLiked = new List<Usuario_Dislike_Nota>();

        public List<Reporte> Reportes { get; set; } = new List<Reporte>();

        public static Nota Crear(NotaRequest request) => new Nota()
        {
            Id = request.Id,
            Contenido = request.Contenido,
            Creador = request.Creador,
            Fecha_Creacion = DateTime.Now,
            Album = request.Album,
            Usuarios_Liked = request.Usuarios_Liked,
            Usuarios_DisLiked = request.Usuarios_DisLiked,
            Reportes = request.Reportes,
            Cantidad_Dislikes = request.Cantidad_Dislikes,
            Cantidad_Likes = request.Cantidad_Likes
            // Agrega otras propiedades si es necesario
        };

        public  NotaResponse ToResponse() => new NotaResponse()
        {
            Id = this.Id,
            Contenido = this.Contenido,
            Creador = this.Creador,
            Fecha_Creacion = this.Fecha_Creacion,
            Album = this.Album,
            Usuarios_Liked = this.Usuarios_Liked,
            Usuarios_DisLiked = this.Usuarios_DisLiked,
            Reportes = this.Reportes,
            Cantidad_Dislikes = this.Cantidad_Dislikes,
            Cantidad_Likes = this.Cantidad_Likes
            // Agrega otras propiedades si es necesario
        };

        public bool Modificar(NotaRequest nota)
        {
            bool modificacion = false;

            if (this.Contenido != nota.Contenido)
            {
                this.Contenido = nota.Contenido;
                modificacion = true;
            }

            if (this.Cantidad_Dislikes != nota.Cantidad_Dislikes)
            {
                this.Cantidad_Dislikes = nota.Cantidad_Dislikes;
                modificacion = true;
            }

            if (this.Cantidad_Likes != nota.Cantidad_Likes)
            {
                this.Cantidad_Likes = nota.Cantidad_Likes;
                modificacion = true;
            }


            if (this.Creador != nota.Creador)
            {
                this.Creador = nota.Creador;
                modificacion = true;
            }

          
            if (this.Album != nota.Album)
            {
                this.Album = nota.Album;
                modificacion = true;
            }

            if (!Usuarios_Liked.SequenceEqual(nota.Usuarios_Liked))
            {
                Usuarios_Liked = nota.Usuarios_Liked;
                modificacion = true;
            }

            if (!Usuarios_DisLiked.SequenceEqual(nota.Usuarios_DisLiked))
            {
                Usuarios_DisLiked = nota.Usuarios_DisLiked;
                modificacion = true;
            }

            if (!Reportes.SequenceEqual(nota.Reportes))
            {
                Reportes = nota.Reportes;
                modificacion = true;
            }

            // Agrega otras comparaciones y actualizaciones si es necesario

            return modificacion;
        }
    

    }
}
