namespace ServoEscolarWebApi.Clases
{
    public class Resultado<T>
    {
        public Resultado(IEnumerable<T> lista, int pagina, int registrosPorPagina, int cantidadPaginas, int totalRegistros)
        {
            Lista = lista;
            Pagina = pagina;
            RegistrosPorPagina = registrosPorPagina;
            CantidadPaginas = cantidadPaginas;
            TotalRegistros = totalRegistros;
        }

        public IEnumerable<T> Lista { get; }

        public int Pagina { get; }

        public int RegistrosPorPagina { get; }

        public int CantidadPaginas { get; }

        public int TotalRegistros { get; }
    }
}
