namespace ServoEscolarWebApi.Datos.Repositorios
{
    public class CiclosEscolaresRepositorio : Repositorio<CicloEscolar, CicloEscolarConsulta, int>
    {
        protected override List<CicloEscolar> ObtenerListado()
        {
            return Datos.CiclosEscolares;
        }
    }
}