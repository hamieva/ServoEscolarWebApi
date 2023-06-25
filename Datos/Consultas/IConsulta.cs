namespace ServoEscolarWebApi.Datos.Consultas
{
    public interface IConsulta<TEntidad, T> where TEntidad : class
    {
        public DateTime? FechaModificacion { get; set; }

        public int Pagina { get; set; }

        public int RegistrosPorPagina { get; set; }

        public Func<TEntidad, bool> ObtenerPredicadoConsulta();

        public Func<TEntidad, Indice<T>> ObtenerPredicadoSelect();
    }
}