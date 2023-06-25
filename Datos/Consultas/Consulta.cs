using System.ComponentModel.DataAnnotations;

namespace ServoEscolarWebApi.Datos.Consultas
{
    public abstract class Consulta<TEntidad, T> : IConsulta<TEntidad, T> where TEntidad : Entidad
    {
        public DateTime? FechaModificacion { get; set; }

        [Range(1, int.MaxValue)]
        [Required]
        public int Pagina { get; set; } = 1;

        [Required]
        [Range(1, 100)]
        public int RegistrosPorPagina { get; set; } = 100;

        protected abstract Func<TEntidad, bool> ObtenerConsultaBase();

        public Func<TEntidad, bool> ObtenerPredicadoConsulta()
        {
            var consultaBase = ObtenerConsultaBase();

            if (FechaModificacion.HasValue)
            {
                if (consultaBase != null)
                {
                    return item => consultaBase(item)
                            && item.FechaModificacion >= FechaModificacion.Value;
                }
                else
                {
                    return item => item.FechaModificacion >= FechaModificacion.Value;
                }
            }

            return consultaBase;
        }

        public abstract Func<TEntidad, Indice<T>> ObtenerPredicadoSelect();
    }
}