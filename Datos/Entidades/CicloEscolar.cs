using System.ComponentModel.DataAnnotations;

namespace ServoEscolarWebApi.Datos.Entidades
{
    public partial class CicloEscolar : Entidad
    {
        [Required]
        public int Año { get; set; }

        [Required]
        [StringLength(50)]
        public string Nombre { get; set; }

        public string Etiqueta { get; set; }

        [Required]
        public DateTime FechaInicial { get; set; }

        [Required]
        public DateTime FechaFinal { get; set; }
    }
}