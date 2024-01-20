using MetaMusic.Data.Request;
using MetaMusic.Data.Responses;
using System.ComponentModel.DataAnnotations;

namespace MetaMusic.Data.Entities
{
    public class Usuario_Like_Nota
    {
        [Key]
        public int Id { get; set; }

        public Usuario? Usuario { get; set; } = new Usuario();

        public Nota? Nota { get; set; } = new Nota();

        public static Usuario_Like_Nota Crear(Nota nota, Usuario usuario) => new Usuario_Like_Nota()
        {
           
            Usuario = usuario,
            Nota = nota
            // Agrega otras propiedades si es necesario
        };

        public bool Modificar(Usuario_Like_NotaRequest usuarioLikeNotaResponse)
        {
            bool modificacion = false;

            if (this.Usuario != usuarioLikeNotaResponse.Usuario)
            {
                this.Usuario = usuarioLikeNotaResponse.Usuario;
                modificacion = true;
            }

            if (this.Nota != usuarioLikeNotaResponse.Nota)
            {
                this.Nota = usuarioLikeNotaResponse.Nota;
                modificacion = true;
            }

            // Agrega otras comparaciones y actualizaciones si es necesario

            return modificacion;
        }

        public Usuario_Like_NotaResponse ToResponse()
        {
            return new Usuario_Like_NotaResponse
            {
                Id = this.Id,
                Usuario = this.Usuario,
                Nota = this.Nota
                // Agrega otras asignaciones si es necesario
            };
        }
    }
}
