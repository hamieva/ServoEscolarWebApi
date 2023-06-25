using Microsoft.AspNetCore.Mvc;

namespace ServoEscolarWebApi.Controllers
{
    public class FamiliasController : CustomController<Familia, FamiliaConsulta, string>
    {
        protected FamiliasRepositorio FamiliasRepository { get; }

        public FamiliasController(IRepositorio<Familia, FamiliaConsulta, string> repository) : base(repository)
        {
            FamiliasRepository = (FamiliasRepositorio)repository;
        }

        [HttpPost("claves")]
        public async Task<IActionResult> ObtenerClaves(FamiliaConsulta consulta)
        {
            return await ObtenerIndice(consulta);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> ObtenerPorId(int? id)
        {
            return await Obtener(id);
        }

        [HttpGet("clave/{clave}")]
        public async Task<IActionResult> ObtenerPorClave(string clave)
        {
            return await Obtener(f => f.Clave.Equals(clave, StringComparison.OrdinalIgnoreCase));
        }

        [HttpGet("familiares/{idFamilia:int}")]
        public async Task<IActionResult> ObtenerFamiliares(int idFamilia)
        {
            var familiares = await FamiliasRepository.ObtenerListadoFamiliares(idFamilia);

            if (familiares == null)
            {
                return NotFound();
            }

            return Ok(familiares);
        }

        protected override async Task<IActionResult> Obtener(int? id)
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

            FamiliasRepositorio.AgregarAlumnosAFamilia(resultado);
            FamiliasRepositorio.AgregarFamiliaresAFamilia(resultado);

            return Ok(resultado);
        }

        protected override async Task<IActionResult> Obtener(Func<Familia, bool> predicado)
        {
            var lista = await Repositorio.Obtener(predicado);

            if (lista == null || !lista.Any())
            {
                return NotFound();
            }

            FamiliasRepositorio.AgregarAlumnosAFamilia(lista);
            FamiliasRepositorio.AgregarFamiliaresAFamilia(lista);

            return Ok(lista);
        }

        protected override async Task<IActionResult> Buscar(FamiliaConsulta consulta)
        {
            var resultado = await Repositorio.Obtener(consulta);

            if (resultado.TotalRegistros <= 0)
            {
                return NotFound();
            }

            FamiliasRepositorio.AgregarAlumnosAFamilia(resultado.Lista);
            FamiliasRepositorio.AgregarFamiliaresAFamilia(resultado.Lista);

            return Ok(resultado);
        }
    }
}
