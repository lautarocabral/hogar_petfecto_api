using hogar_petfecto_api.Models.Perfiles;

namespace hogar_petfecto_api.Models
{
    public class Postulacion
    {
        private Postulacion()
        {
        }

        public Postulacion(
            Adoptante adoptante,
            Mascota mascota,
            DateTime fecha,
            EstadoPostulacion estado)
        {
            Adoptante = adoptante;
            Mascota = mascota;
            Fecha = fecha;
            Estado = estado;
        }

        public int Id { get; private set; }
        public Adoptante Adoptante { get; private set; }
        public Mascota Mascota { get; private set; }
        public DateTime Fecha { get; private set; }
        public EstadoPostulacion Estado { get; private set; }
    }

}
