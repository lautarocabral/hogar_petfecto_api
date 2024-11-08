namespace hogar_petfecto_api.Models.Dtos
{
    public class SuscripcionDto
    {
        public int Id { get;  set; }
        public DateTime FechaInicio { get;  set; }
        public DateTime FechaFin { get;  set; }
        public double Monto { get;  set; }
        public bool Estado { get;  set; }
    }
}
