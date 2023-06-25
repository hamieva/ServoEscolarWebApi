using Microsoft.AspNetCore.Mvc;

namespace ServoEscolarWebApi.Controllers
{
    public class AlumnosController : CustomController<Alumno, AlumnoConsulta, string>
    {
        public AlumnosController(IRepositorio<Alumno, AlumnoConsulta, string> repository) : base(repository)
        {
        }

        [HttpPost("matriculas")]
        public async Task<IActionResult> ObtenerMatriculas(AlumnoConsulta consulta)
        {
           return await ObtenerIndice(consulta);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> ObtenerPorId(int? id)
        {
            return await Obtener(id);
        }

        [HttpGet("matricula/{matricula}")]
        public async Task<IActionResult> ObtenerPorMatricula(string matricula)
        {
            return await Obtener(a => a.Matricula == matricula);
        }

        [HttpPost("buscar")]
        public async Task<IActionResult> BuscarAlumnos(AlumnoConsulta consulta)
        {
            var resultado = await Buscar(consulta);
            return resultado;
        }
    }
}