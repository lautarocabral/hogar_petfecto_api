namespace hogar_petfecto_api.Models
{
    public abstract class Perfil
    {
        protected Perfil()
        {
        }

        protected Perfil(TipoPerfil tipoPerfil, Persona persona)
        {
            TipoPerfil = tipoPerfil;
            Persona = persona;
        }

        public int Id { get; private set; }
        public TipoPerfil TipoPerfil { get; private set; }
        public Persona Persona { get; private set; }

    }
}
