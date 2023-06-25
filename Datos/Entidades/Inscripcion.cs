using System.ComponentModel.DataAnnotations;

namespace ServoEscolarWebApi.Datos.Entidades
{
    public class Inscripcion : Entidad
    {
        [Required]
        public int AlumnoId { get; set; }

        [Required]
        public int CicloEscolarId { get; set; }

        [Required]
        [StringLength(20)]
        public string Seccion { get; set; }

        [Required]
        public int Grado { get; set; }

        [Required]
        [StringLength(5)]
        public string Grupo { get; set; }

        [Required]
        public DateTime FechaInscripcion { get; set; }

        [Required]
        [StringLength(20)]
        public string TipoIngreso { get; set; }

        [Required]
        public bool Baja { get; set; }

        public DateTime? FechaBaja { get; set; }

    }
}
