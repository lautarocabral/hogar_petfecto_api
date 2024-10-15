namespace hogar_petfecto_api.Models
{
    public class TipoPerfil
    {
        private TipoPerfil()
        {
        }

        public TipoPerfil(string descripcion)
        {
            Descripcion = descripcion;
        }

        public int Id { get; private set; }
        public string Descripcion { get; private set; }
    }

}
