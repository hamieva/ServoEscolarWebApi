using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ServoEscolarWebApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public abstract class CustomController<TEntidad, TConsulta, T>
    : ControllerBase where TEntidad : Entidad where TConsulta : Consulta<TEntidad, T> 
    {
        protected IRepositorio<TEntidad, TConsulta, T> Repositorio { get; }

        public CustomController(IRepositorio<TEntidad, TConsulta, T> repository)
        {
            Repositorio = repository;
        }

        protected async Task<IActionResult> ObtenerIndice(TConsulta consulta)
        {
            var resultado = await Repositorio.ObtenerIndice(consulta);

            if (resultado.TotalRegistros == 0)
            {
                return NotFound();
            }

            return Ok(resultado);
        }

        protected virtual async Task<IActionResult> Obtener(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var resultado = await Repositorio.ObtenerPorId(id.Value);

            if (resultado == null)
            {
                return NotFound();
            }

            return Ok(resultado);
        }

        protected virtual async Task<IActionResult> Obtener(Func<TEntidad, bool> predicado)
        {
            var lista = await Repositorio.Obtener(predicado);

            if (lista == null || !lista.Any())
            {
                return NotFound();
            }

            return Ok(lista);
        }

        protected virtual async Task<IActionResult> Buscar(TConsulta consulta)
        {
            var resultado = await Repositorio.Obtener(consulta);

            if (resultado.TotalRegistros <=0)
            {
                return NotFound();
            }

            return Ok(resultado);
        }
    }
}