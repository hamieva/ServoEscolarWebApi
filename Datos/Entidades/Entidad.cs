using System.ComponentModel.DataAnnotations;

namespace ServoEscolarWebApi.Datos.Entidades
{
    public abstract class Entidad : IEntidad
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public DateTime FechaModificacion { get; set; }
    }
}
