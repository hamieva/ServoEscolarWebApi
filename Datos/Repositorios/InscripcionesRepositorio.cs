namespace ServoEscolarWebApi.Datos.Repositorios
{
    public class InscripcionesRepositorio : Repositorio<Inscripcion, InscripcionConsulta, int>
    {
        protected override List<Inscripcion> ObtenerListado()
        {
            return Datos.Inscripciones;
        }
    }
}
