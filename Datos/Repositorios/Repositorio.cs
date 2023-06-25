namespace ServoEscolarWebApi.Datos.Repositorios
{
    public abstract class Repositorio<TEntidad, TConsulta, T> : IRepositorio<TEntidad, TConsulta, T>
        where TEntidad : Entidad where TConsulta : Consulta<TEntidad, T>
    {
        private List<TEntidad> lista;

        protected List<TEntidad> Lista
        {
            get
            {
                lista ??= ObtenerListado();
                return lista;
            }
        }

        public virtual Task<Resultado<TEntidad>> Obtener(TConsulta consulta)
        {
            var predicado = consulta.ObtenerPredicadoConsulta();
            var total = ObtenerTotal(predicado);
            var datos = ObtenerDatos(consulta);

            var paginas = ObtenerCantidadPaginas(total,
                                                 consulta.RegistrosPorPagina);

            var resultado = new Resultado<TEntidad>(datos,
                                                    consulta.Pagina,
                                                    consulta.RegistrosPorPagina,
                                                    paginas,
                                                    total);
            return Task.FromResult(resultado);
        }

        public virtual Task<Resultado<Indice<T>>> ObtenerIndice(TConsulta consulta)
        {
            var datos = ObtenerDatos(consulta);
            var predicado = consulta.ObtenerPredicadoConsulta();
            var total = ObtenerTotal(predicado);
            var select = datos.Select(consulta.ObtenerPredicadoSelect());

            var paginas = ObtenerCantidadPaginas(total,
                                                 consulta.RegistrosPorPagina);

            var resultado = new Resultado<Indice<T>>(select,
                                                     consulta.Pagina,
                                                     consulta.RegistrosPorPagina,
                                                     paginas,
                                                     total);

            return Task.FromResult(resultado);
        }

        public virtual Task<TEntidad> ObtenerPorId(int id)
        {
            var entidad = Lista.FirstOrDefault(a => a.Id == id);
            return Task.FromResult(entidad);
        }

        public Task<IEnumerable<TEntidad>> Obtener(Func<TEntidad, bool> predicado)
        {
            var lista = Lista.Where(predicado);
            return Task.FromResult(lista);
        }

        protected abstract List<TEntidad> ObtenerListado();

        private int ObtenerTotal(Func<TEntidad, bool> predicado)
        {
            var total = Lista
               .AsEnumerable()
               .Count(predicado);
            return total;
        }

        private IEnumerable<TEntidad> ObtenerDatos(TConsulta consulta)
        {
            return Lista
                .AsEnumerable()
                .Where(consulta.ObtenerPredicadoConsulta())
                .Skip((consulta.Pagina - 1) * consulta.RegistrosPorPagina)
                .Take(consulta.RegistrosPorPagina);
        }

        private static int ObtenerCantidadPaginas(int total, int registrosPorPagina)
        {
            int paginas = (int)Math.Ceiling((double)total / registrosPorPagina);
            return paginas;
        }

    }
}
