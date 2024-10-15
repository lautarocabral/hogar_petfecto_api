namespace hogar_petfecto_api.Models
{
    public class Mascota
    {
        private Mascota()
        {
        }

        public Mascota(
            TipoMascota tipoMascota,
            string nombre,
            double peso,
            bool aptoDepto,
            bool aptoPerros,
            DateTime fechaNacimiento,
            bool castrado,
            string sexo,
            bool vacunado,
            bool adoptado)
        {
            TipoMascota = tipoMascota;
            Nombre = nombre;
            Peso = peso;
            AptoDepto = aptoDepto;
            AptoPerros = aptoPerros;
            FechaNacimiento = fechaNacimiento;
            Castrado = castrado;
            Sexo = sexo;
            Vacunado = vacunado;
            Adoptado = adoptado;
        }

        public int Id { get; private set; }
        public TipoMascota TipoMascota { get; private set; }
        public string Nombre { get; private set; }
        public double Peso { get; private set; }
        public bool AptoDepto { get; private set; }
        public bool AptoPerros { get; private set; }
        public DateTime FechaNacimiento { get; private set; }
        public bool Castrado { get; private set; }
        public string Sexo { get; private set; }
        public bool Vacunado { get; private set; }
        public bool Adoptado { get; private set; }
    }

}
