using hogar_petfecto_api.Models.Perfiles;

namespace hogar_petfecto_api.Models
{
    public class Suscripcion
    {
        public int Id { get; private set; }
        public DateTime FechaInicio { get; private set; }
        public DateTime FechaFin { get; private set; }
        public double Monto { get; private set; }
        public bool Estado { get; private set; }
        public TipoPlan TipoPlan { get; private set; }
        public int VeterinariaId { get; private set; } // Clave foránea
        public Veterinaria Veterinaria { get; private set; } // Propiedad de navegación

        // Constructor predeterminado requerido por EF Core
        private Suscripcion() { }

        // Constructor principal para inicialización
        public Suscripcion(DateTime fechaInicio, DateTime fechaFin, double monto, bool estado, TipoPlan tipoPlan, Veterinaria veterinaria)
        {
            FechaInicio = fechaInicio;
            FechaFin = fechaFin;
            Monto = monto;
            Estado = estado;
            TipoPlan = tipoPlan;
            Veterinaria = veterinaria ?? throw new ArgumentNullException(nameof(veterinaria));
            VeterinariaId = veterinaria.Id;
        }

        // Métodos adicionales para cambiar propiedades si es necesario
        public void CambiarEstado(bool nuevoEstado, DateTime nuevaFechaFin)
        {
            Estado = nuevoEstado;
            FechaFin = nuevaFechaFin;
        }

        public void CambiarPlan(TipoPlan nuevoPlan, DateTime nuevaFechaFin)
        {
            TipoPlan = nuevoPlan;
            FechaFin = nuevaFechaFin;
        }

        public void ExtenderSuscripcion(DateTime nuevaFechaFin)
        {
            if (nuevaFechaFin > FechaFin)
            {
                FechaFin = nuevaFechaFin;
            }
        }
    }


    public enum TipoPlan
    {
        Anual,
        Mensual,
    }

}
