using hogar_petfecto_api.Models.Perfiles;

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
            string descripcion,
            bool vacunado,
            bool adoptado,
            string imagen,
            int protectoraId, Protectora protectora)
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
            Imagen = imagen;
            Descripcion = descripcion;
            ProtectoraId = protectoraId;
            TipoMascotaId = tipoMascota.Id;
            Protectora = protectora;
        }

        public int Id { get; private set; }
        public int TipoMascotaId { get; private set; }
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
        public string Imagen { get; private set; }
        public string Descripcion { get; private set; }
        public int ProtectoraId { get; private set; }
        public Protectora Protectora { get; private set; }



        public void Update(
      TipoMascota tipoMascota,
      string nombre,
      double peso,
      bool aptoDepto,
      bool aptoPerros,
      DateTime fechaNacimiento,
      bool castrado,
      string sexo,
      string descripcion,
      bool vacunado,
      string imagen)
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
            Imagen = imagen;
            Descripcion = descripcion;
        }

        public void UpdateEstado(
                bool adoptado
                )
        {

            Adoptado = adoptado;
        }
    }
}
