namespace ServoEscolarWebApi.Datos.Consultas
{
    public class InscripcionConsulta : Consulta<Inscripcion, int>
    {
        public int? AlumnoIdMin { get; set; }

        public int? AlumnoIdMax { get; set; }

        public int? CicloEscolarIdMin { get; set; }

        public int? CicloEscolarIdMax { get; set; }

        public string Seccion { get; set; }

        public int? GradoMin { get; set; }

        public int? GradoMax { get; set; }

        public string Grupo { get; set; }

        public DateTime? FechaInscripcionDesde { get; set; }

        public DateTime? FechaInscripcionHasta { get; set; }

        public bool? Baja { get; set; }

        public DateTime? FechaBajaDesde { get; set; }

        public DateTime? FechaBajaHasta { get; set; }

        public string TipoIngreso { get; set; }

        public override Func<Inscripcion, Indice<int>> ObtenerPredicadoSelect()
        {
            return i => new Indice<int> { Id = i.Id, Valor = i.AlumnoId };
        }

        protected override Func<Inscripcion, bool> ObtenerConsultaBase()
        {
            return inscripcion =>
                (!AlumnoIdMin.HasValue || inscripcion.AlumnoId >= AlumnoIdMin.Value) &&
                (!AlumnoIdMax.HasValue || inscripcion.AlumnoId <= AlumnoIdMax.Value) &&
                (!CicloEscolarIdMin.HasValue || inscripcion.CicloEscolarId >= CicloEscolarIdMin.Value) &&
                (!CicloEscolarIdMax.HasValue || inscripcion.CicloEscolarId <= CicloEscolarIdMax.Value) &&
                (string.IsNullOrEmpty(Seccion) || inscripcion.Seccion == Seccion) &&
                (!GradoMin.HasValue || inscripcion.Grado >= GradoMin.Value) &&
                (!GradoMax.HasValue || inscripcion.Grado <= GradoMax.Value) &&
                (string.IsNullOrEmpty(Grupo) || inscripcion.Grupo == Grupo) &&
                (!FechaInscripcionDesde.HasValue || inscripcion.FechaInscripcion >= FechaInscripcionDesde.Value) &&
                (!FechaInscripcionHasta.HasValue || inscripcion.FechaInscripcion <= FechaInscripcionHasta.Value) &&
                (!Baja.HasValue || inscripcion.Baja == Baja.Value) &&
                (!FechaBajaDesde.HasValue || inscripcion.FechaBaja >= FechaBajaDesde.Value) &&
                (!FechaBajaHasta.HasValue || inscripcion.FechaBaja <= FechaBajaHasta.Value) &&
                (string.IsNullOrEmpty(TipoIngreso) || inscripcion.TipoIngreso == TipoIngreso);
        }
    }
}
