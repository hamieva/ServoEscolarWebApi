namespace ServoEscolarWebApi.Datos.Repositorios
{
    public class AlumnosRepositorio : Repositorio<Alumno, AlumnoConsulta, string>
    {
        protected override List<Alumno> ObtenerListado()
        {
            return Datos.Alumnos;
        }
    }
}