using MetaMusic.Data.Context;
using MetaMusic.Data.Request;
using System.ComponentModel.DataAnnotations;

namespace MetaMusic.Data.Entities
{
    public class Artista_Suscriptor
    {
        
        [Key] public int Id { get; set; }

        public Artista Artista { get; set; } = new Artista();

        public Usuario Usuario { get; set; } = new Usuario();

        public static Artista_Suscriptor Crear(Artista_SuscriptorRequest request) => new Artista_Suscriptor()
        {
            Id = request.Id,
            Artista = request.Artista,
            Usuario = request.Usuario
            // Agrega otras propiedades si es necesario
        };

        public bool Modificar(Artista_SuscriptorRequest artistaSuscriptor)
        {
            bool modificacion = false;

            if (this.Artista != artistaSuscriptor.Artista)
            {
                this.Artista = artistaSuscriptor.Artista;
                modificacion = true;
            }

            if (this.Usuario != artistaSuscriptor.Usuario)
            {
                this.Usuario = artistaSuscriptor.Usuario;
                modificacion = true;
            }

           

            return modificacion;
        }
    }
}
