using MetaMusic.Data.Request;
using MetaMusic.Data.Responses;
using System.ComponentModel.DataAnnotations;

namespace MetaMusic.Data.Entities
{
    public class Reporte
    {
        [Key]
        public int Id { get; set; }

        [StringLength(50)]
        public string Titulo { get; set; } = null!;

        [StringLength(120)]
        public string Contenido { get; set; } = null!;

        public int Severidad { get; set; } = 0;
        public DateTime Fecha_Creacion { get; set; }
        public Usuario? Usuario { get; set; } = new Usuario();

        public Review? Review { get; set; } = new Review();
        public Nota? Nota { get; set; } = new Nota();

        public static Reporte Crear(ReporteRequest request) => new Reporte()
        {
            Id = request.Id,
            Titulo = request.Titulo,
            Contenido = request.Contenido,
            Usuario = request.Usuario,
            Review = request.Review,
            Nota = request.Nota,
            Fecha_Creacion = DateTime.Now,
            Severidad = request.Severidad
            // Agrega otras propiedades si es necesario
        };

        public bool Modificar(ReporteRequest reporte)
        {
            bool modificacion = false;

            if (this.Titulo != reporte.Titulo)
            {
                this.Titulo = reporte.Titulo;
                modificacion = true;
            }

            if (this.Contenido != reporte.Contenido)
            {
                this.Contenido = reporte.Contenido;
                modificacion = true;
            }



            if (this.Usuario != reporte.Usuario)
            {
                this.Usuario = reporte.Usuario;
                modificacion = true;
            }

            if (this.Review != reporte.Review)
            {
                this.Review = reporte.Review;
                modificacion = true;
            }

            if (this.Nota != reporte.Nota)
            {
                this.Nota = reporte.Nota;
                modificacion = true;
            }

            // Agrega otras comparaciones y actualizaciones si es necesario

            return modificacion;
        }

        public ReporteResponse ToResponse()
        {
            return new ReporteResponse
            {
                Id = this.Id,
                Titulo = this.Titulo,
                Contenido = this.Contenido,
                Usuario = this.Usuario,
                Review = this.Review,
                Nota = this.Nota,
                Fecha_Creacion = this.Fecha_Creacion,
                Severidad = this.Severidad
                // Agrega otras asignaciones si es necesario
            };
        }
    }
}
