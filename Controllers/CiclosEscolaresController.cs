using Microsoft.AspNetCore.Mvc;

namespace ServoEscolarWebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CiclosEscolaresController : CustomController<CicloEscolar, CicloEscolarConsulta, int>
    {
        public CiclosEscolaresController(IRepositorio<CicloEscolar, CicloEscolarConsulta, int> repository) : base(repository)
        {
        }

        [HttpPost("años")]
        public async Task<IActionResult> ObtenerAños(CicloEscolarConsulta consulta)
        {
            return await ObtenerIndice(consulta);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> ObtenerPorId(int? id)
        {
            return await Obtener(id);
        }

        [HttpGet("año/{año}")]
        public async Task<IActionResult> ObtenerPorAño(int año)
        {
            return await Obtener(c => c.Año == año);
        }

        [HttpPost("buscar")]
        public async Task<IActionResult> BuscarCiclosEscolares(CicloEscolarConsulta consulta)
        {
            return await Buscar(consulta);
        }

    }
}
