using MetaMusic.Data.Request;
using MetaMusic.Data.Responses;
using System.ComponentModel.DataAnnotations;

namespace MetaMusic.Data.Entities
{
    public class Usuario_Dislike_Nota
    {
        [Key]
        public int Id { get; set; }

        public Usuario? Usuario { get; set; }= new Usuario();



        public Nota? Nota { get; set; }= new Nota();
        public static Usuario_Dislike_Nota Crear(Nota nota, Usuario usuario) => new Usuario_Dislike_Nota()
        {
            
            Usuario = usuario,
            Nota = nota
            // Agrega otras propiedades si es necesario
        };

        public bool Modificar(Usuario_Dislike_NotaRequest usuarioDislikeNotaResponse)
        {
            bool modificacion = false;

            if (this.Usuario != usuarioDislikeNotaResponse.Usuario)
            {
                this.Usuario = usuarioDislikeNotaResponse.Usuario;
                modificacion = true;
            }

            if (this.Nota != usuarioDislikeNotaResponse.Nota)
            {
                this.Nota = usuarioDislikeNotaResponse.Nota;
                modificacion = true;
            }

            // Agrega otras comparaciones y actualizaciones si es necesario

            return modificacion;
        }

        public Usuario_Dislike_NotaResponse ToResponse()
        {
            return new Usuario_Dislike_NotaResponse
            {
                Id = this.Id,
                Usuario = this.Usuario,
                Nota = this.Nota
                // Agrega otras asignaciones si es necesario
            };
        }


    }
}
