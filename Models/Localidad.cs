namespace hogar_petfecto_api.Models
{
    public class Localidad
    {
        private Localidad()
        {
        }

        public Localidad(string localidad, int provinciaId, Provincia provincia)
        {
            this.LocalidadNombre = localidad;
            this.ProvinciaId = provinciaId;
            this.Provincia = provincia;
        }

        public int Id { get; private set; }
        public string LocalidadNombre { get; private set; }
        public int ProvinciaId { get; private set; }
        public Provincia Provincia { get; private set; }
    }

}
