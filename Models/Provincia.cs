namespace hogar_petfecto_api.Models
{
    public class Provincia
    {
        private Provincia()
        {
        }

        public Provincia(string provincia)
        {
            ProvinciaNombre = provincia;
        }

        public int Id { get; private set; }
        public string ProvinciaNombre { get; private set; }

        public virtual ICollection<Localidad> Localidades { get; set; }
    }

}
