namespace ServoEscolarWebApi.Datos.Entidades
{
    public interface IEntidad
    {
        public int Id { get; set; }

        public DateTime FechaModificacion { get; set; }
    }
}
