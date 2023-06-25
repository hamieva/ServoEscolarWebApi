using NuGet.Packaging;

namespace ServoEscolarWebApi.Datos.Repositorios
{
    public class FamiliasRepositorio : Repositorio<Familia, FamiliaConsulta, string>
    {
        protected override List<Familia> ObtenerListado()
        {
            return Datos.Familias;
        }

        public async Task<IEnumerable<Familiar>> ObtenerListadoFamiliares(int idFamilia)
        {
            var resultado = Datos.Familiares.Where(f => f.FamiliaId == idFamilia).ToList();
            return await Task.FromResult(resultado);
        }

        public static IEnumerable<Familia> AgregarAlumnosAFamilia(IEnumerable<Familia> familias)
        {
            foreach (var familia in familias)
            {
                AgregarAlumnosAFamilia(familia);
            }

            return familias;
        }

        public static void AgregarAlumnosAFamilia(Familia familia)
        {
            familia.Alumnos.AddRange(Datos.Alumnos.Where(a => a.FamiliaId == familia.Id));
        }

        public static IEnumerable<Familia> AgregarFamiliaresAFamilia(IEnumerable<Familia> familias)
        {
            foreach (var familia in familias)
            {
                AgregarFamiliaresAFamilia(familia);
            }

            return familias;
        }

        public static void AgregarFamiliaresAFamilia(Familia familia)
        {
            familia.Familiares.AddRange(Datos.Familiares.Where(f => f.FamiliaId == familia.Id));
        }
    }
}