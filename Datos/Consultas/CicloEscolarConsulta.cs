namespace ServoEscolarWebApi.Datos.Consultas
{
    public partial class CicloEscolarConsulta : Consulta<CicloEscolar, int>
    {
        public int IdInicial { get; set; }

        public int IdFinal { get; set; }

        public int AñoInicial { get; set; }

        public int AñoFinal { get; set; }

        public string Nombre { get; set; }

        public string Etiqueta { get; set; }

        public override Func<CicloEscolar, Indice<int>> ObtenerPredicadoSelect()
        {
            return c => new Indice<int> { Id = c.Id, Valor = c.Año };
        }

        protected override Func<CicloEscolar, bool> ObtenerConsultaBase()
        {
            return c =>
                (IdInicial == 0 || c.Id >= IdInicial) &&
                (IdFinal == 0 || c.Id <= IdFinal) &&
                (AñoInicial == 0 || c.Año >= AñoInicial) &&
                (AñoFinal == 0 || c.Año <= AñoFinal) &&
                (string.IsNullOrEmpty(Nombre) || c.Nombre.Contains(Nombre, StringComparison.OrdinalIgnoreCase)) &&
                (string.IsNullOrEmpty(Etiqueta) || c.Etiqueta.Contains(Etiqueta, StringComparison.OrdinalIgnoreCase));
        }
    }
}