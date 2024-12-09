namespace hogar_petfecto_api.Models.Perfiles
{
    public class Adoptante : Perfil
    {
        private Adoptante() : base() { }
        public Adoptante(TipoPerfil tipoPerfil, string estadoCivil, string ocupacion, bool experienciaMascotas, int nroMascotas, Persona persona)
            : base(tipoPerfil, persona)
        {
            EstadoCivil = estadoCivil;
            Ocupacion = ocupacion;
            ExperienciaMascotas = experienciaMascotas;
            NroMascotas = nroMascotas;
        }

        public string EstadoCivil { get; private set; }
        public string Ocupacion { get; private set; }
        public bool ExperienciaMascotas { get; private set; }
        public int NroMascotas { get; private set; }

        public void UpdateAdoptante(string estadoCivil, string ocupacion, bool experienciaMascotas, int nroMascotas)
        {
            EstadoCivil = estadoCivil;
            Ocupacion = ocupacion;
            ExperienciaMascotas = experienciaMascotas;
            NroMascotas = nroMascotas;
        }
    }
}
