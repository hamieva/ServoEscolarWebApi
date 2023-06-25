namespace ServoEscolarWebApi.Datos.Consultas
{
    public class FamiliaConsulta : Consulta<Familia, string>
    {
        public int IdInicial { get; set; }

        public int IdFinal { get; set; }

        public string Clave { get; set; }

        public string Apellidos { get; set; }

        public override Func<Familia, Indice<string>> ObtenerPredicadoSelect()
        {
            return f => new Indice<string> { Id = f.Id, Valor = f.Clave };
        }

        protected override Func<Familia, bool> ObtenerConsultaBase()
        {
            return f =>
            (IdInicial == 0 || f.Id >= IdInicial) &&
            (IdFinal == 0 || f.Id <= IdFinal) &&
            (string.IsNullOrEmpty(Clave) || f.Clave.Equals(Clave, StringComparison.OrdinalIgnoreCase)) &&
            (string.IsNullOrEmpty(Apellidos) || f.Apellidos.Contains(Apellidos, StringComparison.OrdinalIgnoreCase));
        }
    }
}