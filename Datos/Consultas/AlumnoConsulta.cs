using System.ComponentModel.DataAnnotations;

namespace ServoEscolarWebApi.Datos.Consultas
{
    public class AlumnoConsulta : Consulta<Alumno, string>
    {
        public int IdInicial { get; set; }

        public int IdFinal { get; set; }

        public string MatriculaInicial { get; set; }

        public string MatriculaFinal { get; set; }

        [StringLength(50)]
        public string Nombre { get; set; }

        [StringLength(50)]
        public string ApellidoPaterno { get; set; }

        [StringLength(50)]
        public string ApellidoMaterno { get; set; }

        public string Sexo { get; set; }

        public int? FamiliaId { get; set; }

        protected override Func<Alumno, bool> ObtenerConsultaBase()
        {
            return a =>
                (IdInicial == 0 || a.Id >= IdInicial) &&
                (IdFinal == 0 || a.Id <= IdFinal) &&
                (string.IsNullOrEmpty(MatriculaInicial) || string.Compare(a.Matricula, MatriculaInicial) >= 0) &&
                (string.IsNullOrEmpty(MatriculaFinal) || string.Compare(a.Matricula, MatriculaFinal) <= 0) &&
                (string.IsNullOrEmpty(Nombre) || a.Nombre.Contains(Nombre, StringComparison.OrdinalIgnoreCase)) &&
                (string.IsNullOrEmpty(ApellidoPaterno) || a.ApellidoPaterno.Contains(ApellidoPaterno, StringComparison.OrdinalIgnoreCase)) &&
                (string.IsNullOrEmpty(ApellidoMaterno) || a.ApellidoMaterno.Contains(ApellidoMaterno, StringComparison.OrdinalIgnoreCase)) &&
                (string.IsNullOrEmpty(Sexo) || a.Sexo.Equals(Sexo, StringComparison.OrdinalIgnoreCase)) &&
                (!FamiliaId.HasValue || FamiliaId == FamiliaId.Value);
        }

        public override Func<Alumno, Indice<string>> ObtenerPredicadoSelect()
        {
            return a => new Indice<string> { Id = a.Id, Valor = a.Matricula };
        }
    }
}
