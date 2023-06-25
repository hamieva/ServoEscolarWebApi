using Microsoft.AspNetCore.Mvc;

namespace ServoEscolarWebApi.Controllers
{
    public class InscripcionesController : CustomController<Inscripcion, InscripcionConsulta, int>
    {
        public InscripcionesController(IRepositorio<Inscripcion, InscripcionConsulta, int> repository) : base(repository)
        {
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> ObtenerPorId(int? id)
        {
            return await Obtener(id);
        }

        [HttpPost("buscar")]
        public async Task<IActionResult> BuscarInscripciones(InscripcionConsulta consulta)
        {
            return await Buscar(consulta);
        }
    }
}