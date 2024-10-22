namespace hogar_petfecto_api.Models.Perfiles
{
    public class Veterinaria : Perfil
    {
        private Veterinaria() : base(default) { }
        public Veterinaria(TipoPerfil tipoPerfil, double latitud, double longitud, List<Suscripcion> suscripciones, string direccionLocal)
            : base(tipoPerfil) 
        {
            Latitud = latitud;
            Longitud = longitud;
            Suscripciones = suscripciones ?? new List<Suscripcion>();
            DireccionLocal = direccionLocal;
        }

        public double Latitud { get; private set; }
        public double Longitud { get; private set; }
        public List<Suscripcion> Suscripciones { get; private set; }
        public string DireccionLocal { get; private set; }

    }
}
