namespace hogar_petfecto_api.Models.Perfiles
{
    public class Veterinaria : Perfil
    {
        private Veterinaria() : base() { }
        public Veterinaria(TipoPerfil tipoPerfil, double latitud, double longitud, List<Suscripcion> suscripciones, string direccionLocal, List<Oferta> ofertas, string nombre, string telefono, Persona persona)
            : base(tipoPerfil, persona)
        {
            Latitud = latitud;
            Longitud = longitud;
            Suscripciones = suscripciones ?? new List<Suscripcion>();
            DireccionLocal = direccionLocal;
            Ofertas = ofertas;
            Nombre = nombre;
            Telefono = telefono;
        }

        public double Latitud { get; private set; }
        public double Longitud { get; private set; }
        public List<Suscripcion> Suscripciones { get; private set; }
        public string DireccionLocal { get; private set; }
        public List<Oferta> Ofertas { get; private set; }
        public string Nombre { get; private set; }
        public string Telefono { get; private set; }

        public void UpdateVeterinaria(double latitud, double longitud, List<Suscripcion> suscripciones, string direccionLocal, List<Oferta> ofertas)
        {
            Latitud = latitud;
            Longitud = longitud;
            Suscripciones = suscripciones;
            DireccionLocal = direccionLocal;
            Ofertas = ofertas;
        }
    }
}
