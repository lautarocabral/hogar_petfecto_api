namespace hogar_petfecto_api.Models
{
    public class TipoMascota
    {
        private TipoMascota()
        {
        }

        public TipoMascota(string tipo)
        {
            Tipo = tipo;
        }

        public int Id { get; private set; }
        public string Tipo { get; private set; }
    }

}
