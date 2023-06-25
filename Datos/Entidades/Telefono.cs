using System.ComponentModel.DataAnnotations;

namespace ServoEscolarWebApi.Datos.Entidades
{
    public class Telefono : Entidad
    {
        [Required]
        [StringLength(20)]
        public string Etiqueta { get; set; }

        [Required]
        [StringLength(10)]
        [Phone]
        public string Numero { get; set; }
    }
}
