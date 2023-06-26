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
            return i =>
                (!AlumnoIdMin.HasValue || i.AlumnoId >= AlumnoIdMin.Value) &&
                (!AlumnoIdMax.HasValue || i.AlumnoId <= AlumnoIdMax.Value) &&
                (!CicloEscolarIdMin.HasValue || i.CicloEscolarId >= CicloEscolarIdMin.Value) &&
                (!CicloEscolarIdMax.HasValue || i.CicloEscolarId <= CicloEscolarIdMax.Value) &&
                (string.IsNullOrEmpty(Seccion) || i.Seccion == Seccion) &&
                (!GradoMin.HasValue || i.Grado >= GradoMin.Value) &&
                (!GradoMax.HasValue || i.Grado <= GradoMax.Value) &&
                (string.IsNullOrEmpty(Grupo) || i.Grupo == Grupo) &&
                (!FechaInscripcionDesde.HasValue || i.FechaInscripcion >= FechaInscripcionDesde.Value) &&
                (!FechaInscripcionHasta.HasValue || i.FechaInscripcion <= FechaInscripcionHasta.Value) &&
                (!Baja.HasValue || i.Baja == Baja.Value) &&
                (!FechaBajaDesde.HasValue || i.FechaBaja >= FechaBajaDesde.Value) &&
                (!FechaBajaHasta.HasValue || i.FechaBaja <= FechaBajaHasta.Value) &&
                (string.IsNullOrEmpty(TipoIngreso) || i.TipoIngreso == TipoIngreso);
        }
    }
}
