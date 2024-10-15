using hogar_petfecto_api.Models.Perfiles;

namespace hogar_petfecto_api.Models
{
    public class Adopcion
    {
        private Adopcion()
        {
        }

        public Adopcion(
            Mascota mascota,
            Adoptante adoptante,
            DateTime fecha,
            string contrato)
        {
            Mascota = mascota;
            Adoptante = adoptante;
            Fecha = fecha;
            Contrato = contrato;
        }

        public int Id { get; private set; }
        public Mascota Mascota { get; private set; }
        public Adoptante Adoptante { get; private set; }
        public DateTime Fecha { get; private set; }
        public string Contrato { get; private set; }
    }

}
