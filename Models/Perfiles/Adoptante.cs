namespace hogar_petfecto_api.Models.Perfiles
{
    public class Adoptante : Perfil
    {
        // Constructor público que inicializa Adoptante y llama al constructor de la clase base Perfil
        public Adoptante(TipoPerfil tipoPerfil, DateTime fechaNacimiento, string estadoCivil, string ocupacion, bool experienciaMascotas, int nroMascotas)
            : base(tipoPerfil)
        {
            FechaNacimiento = fechaNacimiento;
            EstadoCivil = estadoCivil;
            Ocupacion = ocupacion;
            ExperienciaMascotas = experienciaMascotas;
            NroMascotas = nroMascotas;
        }

        public DateTime FechaNacimiento { get; private set; }
        public string EstadoCivil { get; private set; }
        public string Ocupacion { get; private set; }
        public bool ExperienciaMascotas { get; private set; }
        public int NroMascotas { get; private set; }
    }
}
