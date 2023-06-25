using System.ComponentModel.DataAnnotations;

namespace ServoEscolarWebApi.Datos.Entidades
{
    public partial class Familia : Entidad
    {
        [Required]
        [StringLength(10)]
        public string Clave { get; set; }

        [Required]
        [StringLength(100)]
        public string Apellidos { get; set; }

        public ICollection<Alumno> Alumnos { get; set; }

        public ICollection<Familiar> Familiares { get; set; }
    }
}