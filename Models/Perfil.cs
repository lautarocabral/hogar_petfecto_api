namespace hogar_petfecto_api.Models
{
    public abstract class Perfil
    {
        protected Perfil() 
        {
        }

        protected Perfil(TipoPerfil tipoPerfil)
        {
            TipoPerfil = tipoPerfil;
        }

        public int Id { get; private set; }
        public TipoPerfil TipoPerfil { get; private set; }
    }
}
