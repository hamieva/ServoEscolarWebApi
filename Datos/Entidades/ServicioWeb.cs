using System.ComponentModel.DataAnnotations;

namespace ServoEscolarWebApi.Datos.Entidades
{
    public class ServicioWeb : Entidad
    {
        [Required]
        [StringLength(20)]
        public string Etiqueta { get; set; }

        [Required]
        [StringLength(100)]
        public string Cuenta { get; set; }
    }
}
