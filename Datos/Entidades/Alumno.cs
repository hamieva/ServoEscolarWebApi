using System.ComponentModel.DataAnnotations;

namespace ServoEscolarWebApi.Datos.Entidades
{
    public class Alumno : Entidad
    {
        [Required]
        [StringLength(20)]
        public string Matricula { get; set; }

        [Required]
        [StringLength(50)]
        public string Nombre { get; set; }

        [StringLength(50)]
        public string ApellidoPaterno { get; set; }

        [StringLength(50)]
        public string ApellidoMaterno { get; set; }

        public DateTime FechaNacimiento { get; set; }

        [StringLength(10)]
        public string EstadoNacimiento { get; set; }

        [Required]
        public string Sexo { get; set; }

        public string RFC { get; set; }

        public string CURP { get; set; }

        [Required]
        public int FamiliaId { get; set; }

    }
}
