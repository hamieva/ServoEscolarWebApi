namespace ServoEscolarWebApi.Datos.Repositorios
{
    public interface IRepositorio<TEntidad, TConsulta, T> 
        where TEntidad : Entidad where TConsulta : Consulta<TEntidad, T>
    {
        
        Task<Resultado<Indice<T>>> ObtenerIndice(TConsulta consulta);

        Task<TEntidad> ObtenerPorId(int id);

        Task<Resultado<TEntidad>> Obtener(TConsulta consulta);

        Task<IEnumerable<TEntidad>> Obtener(Func<TEntidad, bool> predicado);
        
    }
}
